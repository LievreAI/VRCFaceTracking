﻿using System.Text.Json;
using Microsoft.Extensions.Logging;
using VRCFaceTracking.Core;
using VRCFaceTracking.Core.Contracts.Services;
using VRCFaceTracking.Core.Models.Osc.FileBased;
using VRCFaceTracking.Core.Models.ParameterDefinition;
using VRCFaceTracking.Core.Params;
using ParamSupervisor = VRCFaceTracking.Core.OSC.DataTypes.ParamSupervisor;

namespace VRCFaceTracking;

/// <summary>
/// ConfigParser is responsible for parsing the traditional JSON OSC config that VRChat produces
/// </summary>
public class AvatarConfigParser
{
    private readonly ILogger<AvatarConfigParser> _logger;
    private string _lastAvatarId;

    public AvatarConfigParser(ILogger<AvatarConfigParser> parserLogger)
    {
        _logger = parserLogger;
    }

    public (IAvatarInfo avatarInfo, List<Parameter> relevantParameters)? ParseNewAvatar(string newId)
    {
        if (newId == _lastAvatarId || string.IsNullOrEmpty(newId))
        {
            return null;
        }

        var paramList = new List<Parameter>();
            
        if (newId.StartsWith("local:"))
        {
            foreach (var parameter in UnifiedTracking.AllParameters_v2.Concat(UnifiedTracking.AllParameters_v1).ToArray())
            {
                paramList.AddRange(parameter.ResetParam(Array.Empty<IParameterDefinition>()));
            }

            // This is a local test avatar, there won't be a config file for it so assume we're using no parameters and just return
            _lastAvatarId = newId;
            return (new NullAvatarDef(newId.Substring(10), newId), paramList);
        }
            
        AvatarConfigFile avatarConfig = null;
        foreach (var userFolder in Directory.GetDirectories(VRChat.VRCOSCDirectory)
                     .Where(folder => Directory.Exists(Path.Combine(folder, "Avatars"))))
        {
            foreach (var avatarFile in Directory.GetFiles(userFolder + "\\Avatars"))
            {    
                var configText = File.ReadAllText(avatarFile);
                var tempConfig = JsonSerializer.Deserialize<AvatarConfigFile>(configText);
                if (tempConfig == null || tempConfig.id != newId)
                {
                    continue;
                }

                avatarConfig = tempConfig;
                break;
            }
        }

        if (avatarConfig == null)
        {
            _logger.LogError("Avatar config file for {avatarId} not found", newId);
            return null;
        }

        _logger.LogInformation("Parsing config file for avatar: {avatarName}", avatarConfig.name);
        ParamSupervisor.SendQueue.Clear();
        var parameters = avatarConfig.parameters.Where(param => param.input != null).ToArray<IParameterDefinition>();

        foreach (var parameter in UnifiedTracking.AllParameters_v2.Concat(UnifiedTracking.AllParameters_v1).ToArray())
        {
            paramList.AddRange(parameter.ResetParam(parameters));
        }

        _lastAvatarId = newId;
        return (avatarConfig, paramList);
    }
}