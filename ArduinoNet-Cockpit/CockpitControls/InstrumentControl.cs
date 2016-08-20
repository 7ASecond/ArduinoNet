/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : InstrumentControl.cs                                           */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : Generic class for the instrument control design                */
/* Creation : 15/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class InstrumentControl : Control
	{
		#region Generic methodes

		/// <summary>
		/// Rotate an image on a point with a specified angle
		/// </summary>
		/// <param name="pe">The paint area event where the image will be displayed</param>
		/// <param name="img">The image to display</param>
		/// <param name="alpha">The angle of rotation in radian</param>
		/// <param name="ptImg">The location of the left upper corner of the image to display in the paint area in nominal situation</param>
		/// <param name="ptRot">The location of the rotation point in the paint area</param>
		/// <param name="scaleFactor">Multiplication factor on the display image</param>
		protected void RotateImage(PaintEventArgs pe, Image img, double alpha, Point ptImg, Point ptRot, float scaleFactor)
		{
			double beta = 0; 	// Angle between the Horizontal line and the line (Left upper corner - Rotation point)
			float deltaX = 0;	// X componant of the corrected translation
			float deltaY = 0;	// Y componant of the corrected translation

			// Compute the correction translation coeff
			if (ptImg != ptRot)
			{
				//
				if (ptRot.X != 0)
				{
					beta = Math.Atan(ptRot.Y / (double)ptRot.X);
				}

				var d = Math.Sqrt((ptRot.X * ptRot.X) + (ptRot.Y * ptRot.Y));		// Distance between Left upper corner and Rotation point)		

				// Computed offset
				deltaX = (float)(d * (Math.Cos(alpha - beta) - Math.Cos(alpha) * Math.Cos(alpha + beta) - Math.Sin(alpha) * Math.Sin(alpha + beta)));
				deltaY = (float)(d * (Math.Sin(beta - alpha) + Math.Sin(alpha) * Math.Cos(alpha + beta) - Math.Cos(alpha) * Math.Sin(alpha + beta)));
			}

			// Rotate image support
			pe.Graphics.RotateTransform((float)(alpha * 180 / Math.PI));

			// Dispay image
			pe.Graphics.DrawImage(img, (ptImg.X + deltaX) * scaleFactor, (ptImg.Y + deltaY) * scaleFactor, img.Width * scaleFactor, img.Height * scaleFactor);

			// Put image support as found
			pe.Graphics.RotateTransform((float)(-alpha * 180 / Math.PI));

		}


		/// <summary>
		/// Translate an image on line with a specified distance and a spcified angle
		/// </summary>
		/// <param name="pe">The paint area event where the image will be displayed</param>
		/// <param name="img">The image to display</param>
		///<param name="deltaPx">The translation distance in pixel</param>
		/// <param name="alpha">The angle of translation direction in radian</param>
		/// <param name="ptImg">The location of the left upper corner of the image to display in the paint area in nominal situation</param>
		/// <param name="scaleFactor">Multiplication factor on the display image</param>
		protected void TranslateImage(PaintEventArgs pe, Image img, int deltaPx, float alpha, Point ptImg, float scaleFactor)
		{
			// Computed offset
			var deltaX = (float)(deltaPx * (Math.Sin(alpha)));
			var deltaY = (float)(- deltaPx * (Math.Cos(alpha)));

			// Dispay image
			pe.Graphics.DrawImage(img, (ptImg.X + deltaX) * scaleFactor, (ptImg.Y + deltaY) * scaleFactor, img.Width * scaleFactor, img.Height * scaleFactor);
		}


		/// <summary>
		/// Rotate an image an apply a translation on the rotated image and the display it
		/// </summary>
		/// <param name="pe">The paint area event where the image will be displayed</param>
		/// <param name="img">The image to display</param>
		/// <param name="alphaRot">The angle of rotation in radian</param>
		/// <param name="alphaTrs">The angle of translation direction in radian, expressed in the rotated image coordonate system</param>
		/// <param name="ptImg">The location of the left upper corner of the image to display in the paint area in nominal situation</param>
		/// <param name="ptRot">The location of the rotation point in the paint area</param>
		/// <param name="deltaPx">The translation distance in pixel</param>
	/// <param name="scaleFactor">Multiplication factor on the display image</param>
		protected void RotateAndTranslate(PaintEventArgs pe, Image img, double alphaRot, double alphaTrs, Point ptImg, int deltaPx, Point ptRot, float scaleFactor)
		{
			double beta = 0;
			float deltaXRot = 0;
			float deltaYRot = 0;

			// Rotation

			if (ptImg != ptRot)
			{
				// Internals coeffs
				if (ptRot.X != 0)
				{
					beta = Math.Atan(ptRot.Y / (double)ptRot.X);
				}

				var d = Math.Sqrt((ptRot.X * ptRot.X) + (ptRot.Y * ptRot.Y));

				// Computed offset
				deltaXRot = (float)(d * (Math.Cos(alphaRot - beta) - Math.Cos(alphaRot) * Math.Cos(alphaRot + beta) - Math.Sin(alphaRot) * Math.Sin(alphaRot + beta)));
				deltaYRot = (float)(d * (Math.Sin(beta - alphaRot) + Math.Sin(alphaRot) * Math.Cos(alphaRot + beta) - Math.Cos(alphaRot) * Math.Sin(alphaRot + beta)));
			}

			// Translation

			// Computed offset
			var deltaXTrs = (float)(deltaPx * (Math.Sin(alphaTrs)));
			var deltaYTrs = (float)(- deltaPx * (-Math.Cos(alphaTrs)));

			// Rotate image support
			pe.Graphics.RotateTransform((float)(alphaRot * 180 / Math.PI));

			// Dispay image
			pe.Graphics.DrawImage(img, (ptImg.X + deltaXRot + deltaXTrs) * scaleFactor, (ptImg.Y + deltaYRot + deltaYTrs) * scaleFactor, img.Width * scaleFactor, img.Height * scaleFactor);

			// Put image support as found
			pe.Graphics.RotateTransform((float)(-alphaRot * 180 / Math.PI));
		}


		/// <summary>
		/// Display a Scroll counter image like on gas  pumps 
		/// </summary>
		/// <param name="pe">The paint area event where the image will be displayed</param>
		/// <param name="imgBand">The band counter image to display with digts : 0|1|2|3|4|5|6|7|8|9|0</param>
		/// <param name="nbOfDigits">The number of digits displayed by the counter</param>
		/// <param name="counterValue">The value to dispay on the counter</param>
		/// <param name="ptImg">The location of the left upper corner of the image to display in the paint area in nominal situation</param>
		/// <param name="scaleFactor">Multiplication factor on the display image</param>
		protected void ScrollCounter(PaintEventArgs pe, Image imgBand, int nbOfDigits, int counterValue, Point ptImg, float scaleFactor)
		{
			int indexDigit;
			var digitBoxHeight = imgBand.Height/11;
			var digitBoxWidth = imgBand.Width;

			for(indexDigit = 0; indexDigit<nbOfDigits; indexDigit++)
			{
				int prevDigit;
				int yOffset;

				var currentDigit = (int)((counterValue / Math.Pow(10, indexDigit)) % 10);

				if (indexDigit == 0)
				{
					prevDigit = 0;
				}
				else
				{
					prevDigit = (int)((counterValue / Math.Pow(10, indexDigit-1)) % 10);
				}

				// xOffset Computing
				var xOffset = digitBoxWidth * (nbOfDigits - indexDigit - 1);
				
				// yOffset Computing	
				if(prevDigit == 9)
				{
					const double fader = 0.33;
					yOffset = (int)(-((fader + currentDigit) * digitBoxHeight));
				}
				else
				{
					yOffset = -(currentDigit * digitBoxHeight);
				}

				// Display Image
				pe.Graphics.DrawImage(imgBand,(ptImg.X + xOffset)*scaleFactor,(ptImg.Y + yOffset)*scaleFactor,imgBand.Width*scaleFactor,imgBand.Height*scaleFactor);
			}
		}

		protected void DisplayRoundMark(PaintEventArgs pe, Image imgMark, InstrumentControlMarksDefinition insControlMarksDefinition, Point ptImg, int radiusPx, bool displayText, float scaleFactor)
		{
			var textBoxHeight = (int)(insControlMarksDefinition.FontSize*1.1/scaleFactor);      
			var textPoint = new Point();
			var rotatePoint = new Point();
			var markFont = new Font("Arial", insControlMarksDefinition.FontSize);
			var markBrush = new SolidBrush(insControlMarksDefinition.FontColor);
			var markArray = new InstrumentControlMarkPoint[2 + insControlMarksDefinition.NumberOfDivisions];

			// Buid the markArray
			markArray[0].Value = insControlMarksDefinition.MinPhy;
			markArray[0].Angle = insControlMarksDefinition.MinAngle;
			markArray[markArray.Length - 1].Value = insControlMarksDefinition.MaxPhy;
			markArray[markArray.Length - 1].Angle = insControlMarksDefinition.MaxAngle;

			for (var index = 1; index < insControlMarksDefinition.NumberOfDivisions+1; index++)
			{
				markArray[index].Value = (insControlMarksDefinition.MaxPhy - insControlMarksDefinition.MinPhy) / (insControlMarksDefinition.NumberOfDivisions + 1) * index + insControlMarksDefinition.MinPhy;
				markArray[index].Angle = (insControlMarksDefinition.MaxAngle - insControlMarksDefinition.MinAngle) / (insControlMarksDefinition.NumberOfDivisions + 1) * index + insControlMarksDefinition.MinAngle;
			}

			// Define the rotate point (center of the indicator)
			// ReSharper disable once PossibleLossOfFraction
			rotatePoint.X = (int)((Width / 2)/scaleFactor);
			rotatePoint.Y = rotatePoint.X;
			
			// Display mark array
			foreach (var markPoint in markArray)
			{
				// pre computings
				var alphaRot = (Math.PI / 2) - markPoint.Angle;
				var textBoxLength = (int)(Convert.ToString(markPoint.Value, CultureInfo.CurrentCulture).Length * insControlMarksDefinition.FontSize*0.8/scaleFactor);
				var textPointRadiusPx = (int)(radiusPx - 1.2*imgMark.Height - 0.5 * textBoxLength);
				textPoint.X = (int)((textPointRadiusPx * Math.Cos(markPoint.Angle) - 0.5 * textBoxLength + rotatePoint.X) * scaleFactor);
				textPoint.Y = (int)((-textPointRadiusPx * Math.Sin(markPoint.Angle) - 0.5 * textBoxHeight + rotatePoint.Y) * scaleFactor);
				
				// Display mark
				RotateImage(pe, imgMark, alphaRot, ptImg, rotatePoint, scaleFactor);

				// Display text
				if (displayText)
				{
					pe.Graphics.DrawString(Convert.ToString(markPoint.Value, CultureInfo.CurrentCulture), markFont, markBrush, textPoint);
				}
			}
		}


		/// <summary>
		/// Convert a physical value in an rad angle used by the rotate function
		/// </summary>
		/// <param name="phyVal">Physical value to interpol</param>
		/// <param name="minPhy">Minimum physical value</param>
		/// <param name="maxPhy">Maximum physical value</param>
		/// <param name="minAngle">The angle related to the minumum value, in deg</param>
		/// <param name="maxAngle">The angle related to the maximum value, in deg</param>
		/// <returns>The angle in radian witch correspond to the physical value</returns>
		protected float InterpolPhyToAngle(float phyVal, float minPhy, float maxPhy, float minAngle, float maxAngle)
		{
			if (phyVal < minPhy)
			{
				return (float)(minAngle * Math.PI / 180);
			}
			if (phyVal > maxPhy)
			{
				return (float)(maxAngle * Math.PI / 180);
			}
			var x = phyVal;
			var a = (maxAngle - minAngle) / (maxPhy - minPhy);
			var b = (float)(0.5 * (maxAngle + minAngle - a * (maxPhy + minPhy)));
			var y = a * x + b;

			return (float)(y * Math.PI / 180);
		}

		protected Point FromCartRefToImgRef(Point cartPoint)
		{
			var imgPoint = new Point
			{
				X = cartPoint.X + (Width/2),
				Y = -cartPoint.Y + (Height/2)
			};
			return (imgPoint);
		}

		protected double FromDegToRad(double degAngle)
		{
			var radAngle = degAngle * Math.PI / 180;
			return radAngle;
		}


		#endregion
	}

	internal struct InstrumentControlMarksDefinition
	{
		public InstrumentControlMarksDefinition(float myMinPhy, float myMinAngle, float myMaxPhy, float myMaxAngle, int myNumberOfDivisions, int myFontSize, Color myFontColor, InstumentMarkScaleStyle myScaleStyle)
		{
			MinPhy = myMinPhy;
			MinAngle = myMinAngle;
			MaxPhy = myMaxPhy;
			MaxAngle = myMaxAngle;
			NumberOfDivisions = myNumberOfDivisions;
			FontSize = myFontSize;
			FontColor = myFontColor;
			ScaleStyle = myScaleStyle;
		}
		internal float MinPhy;
		internal float MinAngle;
		internal float MaxPhy;
		internal float MaxAngle;
		internal int NumberOfDivisions;
		internal int FontSize;
		internal Color FontColor;
		internal InstumentMarkScaleStyle ScaleStyle;
	}

	internal struct InstrumentControlMarkPoint
	{
		public InstrumentControlMarkPoint(float myValue, float myAngle)
		{
			Value = myValue;
			Angle = myAngle;
		}
		internal float Value;
		internal float Angle;
	}

	internal enum InstumentMarkScaleStyle
	{
		Linear = 0,
		Log = 1
	}
}
