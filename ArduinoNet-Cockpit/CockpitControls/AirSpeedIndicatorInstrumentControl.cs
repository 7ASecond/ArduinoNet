/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : AirSpeedIndicatorInstrumentControl.cs                          */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The air speed indicator instrument control                     */
/* Creation : 19/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class AirSpeedIndicatorInstrumentControl : InstrumentControl
	{

		public delegate void ControlConsumer();  // defines a delegate type

		#region Fields

		// Parameters
		private int _airSpeed; 

		// Images
		private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.AirSpeedIndicator_Background);
		private readonly Bitmap _bmpNeedle = new Bitmap(Properties.Resources.AirSpeedNeedle);

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public AirSpeedIndicatorInstrumentControl()
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
			Components = new System.ComponentModel.Container();
		}
		#endregion

		#region Paint

		protected override void OnPaint(PaintEventArgs pe)
		{
			// Calling the base class OnPaint
			base.OnPaint(pe);

			// Pre Display computings
			var ptRotation = new Point(150, 150);
			var ptimgNeedle = new Point(136,39);

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpNeedle.MakeTransparent(Color.Yellow);

			double alphaNeedle = InterpolPhyToAngle(_airSpeed,0,800,180,468);

			var scale = (float)Width / _bmpCadran.Width;

			// diplay mask
			var maskPen = new Pen(BackColor, 30 * scale);
			pe.Graphics.DrawRectangle(maskPen, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display cadran
			pe.Graphics.DrawImage(_bmpCadran, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display small needle
			RotateImage(pe, _bmpNeedle, alphaNeedle, ptimgNeedle, ptRotation, scale);

		}

		#endregion

		#region Methods


		/// <summary>
		/// Define the physical value to be displayed on the indicator
		/// </summary>
		/// <param name="aircraftAirSpeed">The aircraft air speed in kts</param>
		public void SetAirSpeedIndicatorParameters(int aircraftAirSpeed)
		{
			_airSpeed = aircraftAirSpeed;

			SetRefresh();
		}

		#endregion

		private void SetRefresh()
		{
			if (InvokeRequired)
			{
				Invoke(new ControlConsumer(SetRefresh), new object[] { });  // invoking itself
			}
			else
			{
				Refresh();      // the "functional part", executing only on the main thread
			}
		}

		#region IDE




		#endregion

	}
}
