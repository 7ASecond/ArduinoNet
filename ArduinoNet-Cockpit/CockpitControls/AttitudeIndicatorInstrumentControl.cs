/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : AttitudeIndicatorInstrumentControl.cs                          */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The attitude indicator instrument control                      */
/* Creation : 22/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ArduinoNet_Cockpit.CockpitControls.AvionicsInstrumentsControls;

namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class AttitudeIndicatorInstrumentControl : InstrumentControl
	{
		#region Fields

		// Parameters
		private double _pitchAngle; // Phi
		private double _rollAngle; // Theta

		// Images
		private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.Horizon_Background);
		private readonly Bitmap _bmpBoule = new Bitmap(Properties.Resources.Horizon_GroundSky);
		private readonly Bitmap _bmpAvion = new Bitmap(Properties.Resources.Maquette_Avion);

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public AttitudeIndicatorInstrumentControl()
		{
			// Double bufferisation
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint, true);
		}

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Components = new Container();
		}
		#endregion

		#region Paint

		protected override void OnPaint(PaintEventArgs pe)
		{
			// Calling the base class OnPaint
			base.OnPaint(pe);

			// Pre Display computings

			var ptBoule = new Point(25, - 210);
			var ptRotation = new Point(150, 150);

			var scale = (float)Width / _bmpCadran.Width;

			// Affichages - - - - - - - - - - - - - - - - - - - - - - 

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpAvion.MakeTransparent(Color.Yellow);

			// display Horizon
			RotateAndTranslate(pe, _bmpBoule, _rollAngle, 0, ptBoule, (int)(4*_pitchAngle), ptRotation, scale);

			// diplay mask
			var maskPen = new Pen(BackColor,30*scale);
			pe.Graphics.DrawRectangle(maskPen, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display cadran
			pe.Graphics.DrawImage(_bmpCadran, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display aircraft symbol
			pe.Graphics.DrawImage(_bmpAvion, (float)((0.5 * _bmpCadran.Width - 0.5 * _bmpAvion.Width) * scale), (float)((0.5 * _bmpCadran.Height - 0.5 * _bmpAvion.Height) * scale), _bmpAvion.Width * scale, _bmpAvion.Height * scale);


		}

		#endregion

		#region Methods

		/// <summary>
		/// Define the physical value to be displayed on the indicator
		/// </summary>
		/// <param name="aircraftPitchAngle">The aircraft pitch angle in °deg</param>
		/// <param name="aircraftRollAngle">The aircraft roll angle in °deg</param>
		public void SetAttitudeIndicatorParameters(double aircraftPitchAngle, double aircraftRollAngle)
		{
			_pitchAngle = aircraftPitchAngle;
			_rollAngle = aircraftRollAngle * Math.PI / 180;

			Refresh();
		}

		#endregion

	}
}
