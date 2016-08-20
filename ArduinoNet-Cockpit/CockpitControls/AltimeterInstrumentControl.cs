/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : AltimeterInstrumentControl.cs                                  */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The altimeter instrument control                     */
/* Creation : 16/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;



namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class AltimeterInstrumentControl : InstrumentControl
	{
		public delegate void ControlConsumer();  // defines a delegate type

		#region Fields

		// Parameters
		private int _altitude; 

		// Images
		private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.Altimeter_Background);
		private readonly Bitmap _bmpSmallNeedle = new Bitmap(Properties.Resources.SmallNeedleAltimeter);
		private readonly Bitmap _bmpLongNeedle = new Bitmap(Properties.Resources.LongNeedleAltimeter);
		private readonly Bitmap _bmpScroll = new Bitmap(Properties.Resources.Bandeau_Dérouleur);

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public AltimeterInstrumentControl()
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
			var ptCounter = new Point(35, 135);
			var ptRotation = new Point(150, 150);
			var ptimgNeedle = new Point(136,39);

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpLongNeedle.MakeTransparent(Color.Yellow);
			_bmpSmallNeedle.MakeTransparent(Color.Yellow);

			double alphaSmallNeedle = InterpolPhyToAngle(_altitude,0,10000,0,359);
			double alphaLongNeedle = InterpolPhyToAngle(_altitude%1000,0,1000,0,359);

			var scale = (float)Width / _bmpCadran.Width;

			// display counter
			ScrollCounter(pe, _bmpScroll, 5, _altitude, ptCounter, scale);

			// diplay mask
			var maskPen = new Pen(BackColor, 30 * scale);
			pe.Graphics.DrawRectangle(maskPen, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display cadran
			pe.Graphics.DrawImage(_bmpCadran, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display small needle
			RotateImage(pe, _bmpSmallNeedle, alphaSmallNeedle, ptimgNeedle, ptRotation, scale);

			// display long needle
			RotateImage(pe, _bmpLongNeedle, alphaLongNeedle, ptimgNeedle, ptRotation, scale);

		}

		#endregion

		#region Methods


		/// <summary>
		/// Define the physical value to be displayed on the indicator
		/// </summary>
		/// <param name="aircraftAltitude">The aircraft altitude in ft</param>
		public void SetAlimeterParameters(int aircraftAltitude)
		{
			_altitude = aircraftAltitude;

			SetRefresh();
		}

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

		#endregion

	}
}
