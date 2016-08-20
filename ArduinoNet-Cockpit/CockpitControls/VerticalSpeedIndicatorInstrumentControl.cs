/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : VerticalSpeedIndicatorInstrumentControl.cs                     */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The vertical speed indicator instrument control                */
/* Creation : 19/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class VerticalSpeedIndicatorInstrumentControl : InstrumentControl
	{
		#region Fields

		// Parameters
	    private int _verticalSpeed; 

		// Images
	    private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.VerticalSpeedIndicator_Background);
	    private readonly Bitmap _bmpNeedle = new Bitmap(Properties.Resources.VerticalSpeedNeedle);

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public VerticalSpeedIndicatorInstrumentControl()
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
			var ptRotation = new Point(150, 150);
			var ptimgNeedle = new Point(136,39);

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpNeedle.MakeTransparent(Color.Yellow);

			double alphaNeedle = InterpolPhyToAngle(_verticalSpeed,-6000,6000,120,420);

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
		/// <param name="aircraftVerticalSpeed">The aircraft vertical speed in ft per minutes</param>
		public void SetVerticalSpeedIndicatorParameters(int aircraftVerticalSpeed)
		{
			_verticalSpeed = aircraftVerticalSpeed;

			Refresh();
		}

		#endregion

	}
}
