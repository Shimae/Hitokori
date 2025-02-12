﻿using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using System;

namespace osu.Game.Rulesets.Hitokori.Difficulty.Skills {
	public class Reading : Skill {
		protected override double SkillMultiplier => 0.6;
		protected override double StrainDecayBase => 0.6;

		private const double DIRECTION_CHANGE_BONUS = 1.05;

		protected override double StrainValueOf ( DifficultyHitObject current ) {
			HitokoriDifficultyHitObject hitokoriCurrent = (HitokoriDifficultyHitObject)current;

			double strain = CalculateAngleStrain( hitokoriCurrent.HitAngle );

			if ( hitokoriCurrent.ChangedDirection ) strain *= DIRECTION_CHANGE_BONUS;

			if ( hitokoriCurrent.HoldAngle != null ) {
				strain += CalculateAngleStrain( hitokoriCurrent.HoldAngle.Value );
			}

			return 1 + strain;
		}

		//Strain linearly grows from 0.0 to 2/3 the farther the angle is from 180 degrees
		private double CalculateAngleStrain ( double angle ) {
			return Math.Abs( ( Math.Abs( angle ) - Math.PI ) / ( Math.PI * 3 / 2 ) );
		}
	}
}
