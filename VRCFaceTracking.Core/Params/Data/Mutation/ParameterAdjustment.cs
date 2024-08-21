﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VRCFaceTracking.Core.Params.Expressions;
using VRCFaceTracking.SDK;

namespace VRCFaceTracking.Core.Params.Data.Mutation;
public class ParameterAdjustment : TrackingMutation
{
    [MutationButton("Reset Values")]
    public void Reset()
    {
        for (int i = 0; i < (int)UnifiedExpressions.Max; i++)
        {
        }
    }

    [MutationProperty("Eyebrow")] public (float, float) eyeBrows = new(0, 1);
    [MutationProperty("Eye Squint")] public (float, float) eyeSquint = new(0, 1);
    [MutationProperty("Eye Wide")] public (float, float) eyeWide = new(0, 1);
    [MutationProperty("Cheek")] public (float, float) cheekPuffSuck = new(0, 1);
    [MutationProperty("Cheek Squint")] public (float, float) cheekSquint = new(0, 1);
    [MutationProperty("Jaw")] public (float, float) jawOpen = new(0, 1);
    [MutationProperty("Jaw Sideways")] public (float, float) jawX = new(0, 1);
    [MutationProperty("Lip Funnel")] public (float, float) lipFunnel = new(0, 1);
    [MutationProperty("Lip Suck")] public (float, float) lipSuck = new(0, 1);
    [MutationProperty("Lip Pucker")] public (float, float) lipPucker = new(0, 1);
    [MutationProperty("Mouth Open")] public (float, float) mouthOpener = new(0, 1);
    [MutationProperty("Mouth Smile")] public (float, float) mouthSmile = new(0, 1);
    [MutationProperty("Mouth Frown")] public (float, float) mouthFrown = new(0, 1);
    [MutationProperty("Mouth Stretch")] public (float, float) mouthStretch = new(0, 1);
    [MutationProperty("Mouth Tightener")] public (float, float) mouthTightener = new(0, 1);
    [MutationProperty("Mouth Press")] public (float, float) mouthPress = new(0, 1);
    [MutationProperty("Mouth Sideways")] public (float, float) mouthX = new(0, 1);
    [MutationProperty("Mouth Raiser")] public (float, float) mouthRaiser = new(0, 1);
    [MutationProperty("Nose")] public (float, float) nose = new(0, 1);
    [MutationProperty("Nose Sneer")] public (float, float) noseSneer = new(0, 1);
    [MutationProperty("Neck")] public (float, float) neck = new(0, 1);
    [MutationProperty("Tongue Out")] public (float, float) tongueOut = new(0, 1);
    [MutationProperty("Tongue Directions")] public (float, float) tongueMove = new(0, 1);
    [MutationProperty("Tongue Miscellaneous")] public (float, float) tongueOther = new(0, 1);

    public override string Name => "Parameter Adjustment";

    public override string Description => "Adjust VRCFaceTracking Parameters.";

    public override MutationPriority Step => MutationPriority.None;

    float Range(float value, float floor, float ceil) => (value - floor) / (ceil - floor);

    float SetRange(ref UnifiedExpressionShape shape, (float, float) ranges) => shape.Weight = Range(shape.Weight, ranges.Item1, ranges.Item2);

    public override void MutateData(ref UnifiedTrackingData data)
    {
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowInnerUpLeft], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowInnerUpRight], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowOuterUpLeft], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowOuterUpRight], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowLowererLeft], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowLowererRight], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowPinchLeft], eyeBrows);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.BrowPinchRight], eyeBrows);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekPuffLeft], cheekPuffSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekPuffRight], cheekPuffSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekSuckLeft], cheekPuffSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekSuckRight], cheekPuffSuck);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekSquintLeft], cheekSquint);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.CheekSquintRight], cheekSquint);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.EyeSquintLeft], eyeSquint);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.EyeSquintRight], eyeSquint);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.EyeWideLeft], eyeWide);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.EyeWideRight], eyeWide);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawBackward], eyeWide);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawForward], eyeWide);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawClench], jawOpen);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawMandibleRaise], jawOpen);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawOpen], jawOpen);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawOpen], jawOpen);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthClosed], jawOpen);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawLeft], jawX);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.JawRight], jawX);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipFunnelLowerLeft], lipFunnel);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipFunnelLowerRight], lipFunnel);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipFunnelUpperLeft], lipFunnel);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipFunnelUpperRight], lipFunnel);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipPuckerLowerLeft], lipPucker);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipPuckerLowerRight], lipPucker);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipPuckerUpperLeft], lipPucker);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipPuckerUpperRight], lipPucker);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckCornerLeft], lipSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckCornerRight], lipSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckUpperLeft], lipSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckUpperRight], lipSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckLowerLeft], lipSuck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.LipSuckLowerRight], lipSuck);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperDeepenLeft], mouthOpener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperDeepenRight], mouthOpener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperUpRight], mouthOpener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperUpLeft], mouthOpener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthLowerDownRight], mouthOpener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthLowerDownRight], mouthOpener);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthCornerPullLeft], mouthSmile);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthCornerPullRight], mouthSmile);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthCornerSlantLeft], mouthSmile);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthCornerSlantRight], mouthSmile);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthFrownLeft], mouthFrown);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthFrownRight], mouthFrown);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthStretchLeft], mouthStretch);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthStretchRight], mouthStretch);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthTightenerLeft], mouthTightener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthTightenerRight], mouthTightener);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthPressRight], mouthPress);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthPressLeft], mouthPress);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperLeft], mouthX);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthUpperRight], mouthX);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthLowerLeft], mouthX);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthLowerRight], mouthX);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthRaiserLower], mouthRaiser);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.MouthRaiserUpper], mouthRaiser);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.NasalConstrictLeft], nose);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.NasalConstrictRight], nose);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.NasalDilationLeft], nose);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.NasalDilationRight], nose);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.NoseSneerLeft], noseSneer);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.NoseSneerRight], noseSneer);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.NeckFlexLeft], neck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.NeckFlexRight], neck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.SoftPalateClose], neck);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.ThroatSwallow], neck);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueBendDown], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueCurlUp], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueDown], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueUp], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueLeft], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueRight], tongueMove);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueTwistLeft], tongueOther);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueTwistRight], tongueOther);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueOut], tongueOut);

        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueFlat], tongueOther);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueSquish], tongueOther);
        SetRange(ref data.Shapes[(int)UnifiedExpressions.TongueRoll], tongueOther);
    }
}
