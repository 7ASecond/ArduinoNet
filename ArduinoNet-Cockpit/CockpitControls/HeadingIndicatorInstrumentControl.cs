/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : HeadingIndicatorInstrumentControl.cs                           */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The heading indicator instrument control                       */
/* Creation : 25/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;



namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class HeadingIndicatorInstrumentControl : InstrumentControl
	{
		#region Fields

		// Parameters
		private int _heading; 

		// Images
		private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.HeadingIndicator_Background);
		private readonly Bitmap _bmpHedingWeel = new Bitmap(Properties.Resources.HeadingWeel);
		private readonly Bitmap _bmpAircaft = new Bitmap(Properties.Resources.HeadingIndicator_Aircraft);        

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public HeadingIndicatorInstrumentControl()
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
			var ptImgAircraft = new Point(73,41);
			var ptImgHeadingWeel = new Point(13, 13);

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpHedingWeel.MakeTransparent(Color.Yellow);
			_bmpAircaft.MakeTransparent(Color.Yellow);

			double alphaHeadingWeel = InterpolPhyToAngle(_heading,0,360,360,0);

			var scale = (float)Width / _bmpCadran.Width;

			// diplay mask
			var maskPen = new Pen(BackColor, 30 * scale);
			pe.Graphics.DrawRectangle(maskPen, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display cadran
			pe.Graphics.DrawImage(_bmpCadran, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display HeadingWeel
			RotateImage(pe,_bmpHedingWeel, alphaHeadingWeel, ptImgHeadingWeel, ptRotation, scale);

			// display aircraft
			pe.Graphics.DrawImage(_bmpAircaft, (int)(ptImgAircraft.X*scale), (int)(ptImgAircraft.Y*scale), _bmpAircaft.Width * scale, _bmpAircaft.Height * scale);

		}

		#endregion

		#region Methods

		/// <summary>
		/// Define the physical value to be displayed on the indicator
		/// </summary>
		/// <param name="aircraftHeading">The aircraft heading in °deg</param>
		public void SetHeadingIndicatorParameters(int aircraftHeading)
		{
			_heading = aircraftHeading;

			Refresh();
		}

		#endregion
	}
}
