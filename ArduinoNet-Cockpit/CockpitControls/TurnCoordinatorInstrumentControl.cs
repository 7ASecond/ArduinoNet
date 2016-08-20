/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : TurnCoordinatorInstrumentControl.cs                            */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The turn coordinator instrument control                        */
/* Creation : 15/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;



namespace ArduinoNet_Cockpit.CockpitControls
{
	internal class TurnCoordinatorInstrumentControl : InstrumentControl
	{
		#region Fields

		// Parameters
	    private float _turnRate;
	    private float _turnQuality;

		// Images
	    private readonly Bitmap _bmpCadran = new Bitmap(Properties.Resources.TurnCoordinator_Background);
	    private readonly Bitmap _bmpBall = new Bitmap(Properties.Resources.TurnCoordinatorBall);
	    private readonly Bitmap _bmpAircraft = new Bitmap(Properties.Resources.TurnCoordinatorAircraft);
	    private readonly Bitmap _bmpMarks = new Bitmap(Properties.Resources.TurnCoordinatorMarks);

		#endregion

		#region Contructor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Container Components;

		public TurnCoordinatorInstrumentControl()
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
			var ptRotationAircraft = new Point(150, 150);
			var ptImgAircraft = new Point(57,114);
			var ptRotationBall = new Point(150, -155);
			var ptImgBall = new Point(136, 216);
			var ptMarks = new Point(134, 216);

			_bmpCadran.MakeTransparent(Color.Yellow);
			_bmpBall.MakeTransparent(Color.Yellow);
			_bmpAircraft.MakeTransparent(Color.Yellow);
			_bmpMarks.MakeTransparent(Color.Yellow);

			double alphaAircraft = InterpolPhyToAngle(_turnRate,-6,6,-30,30);
			double alphaBall = InterpolPhyToAngle(_turnQuality, -10, 10, -11, 11);

			var scale = (float)Width / _bmpCadran.Width;

			// diplay mask
			var maskPen = new Pen(BackColor, 30 * scale);
			pe.Graphics.DrawRectangle(maskPen, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display cadran
			pe.Graphics.DrawImage(_bmpCadran, 0, 0, _bmpCadran.Width * scale, _bmpCadran.Height * scale);

			// display Ball
			RotateImage(pe,_bmpBall, alphaBall, ptImgBall, ptRotationBall, scale);

			// display Aircraft
			RotateImage(pe, _bmpAircraft, alphaAircraft, ptImgAircraft, ptRotationAircraft, scale);

			// display Marks
			pe.Graphics.DrawImage(_bmpMarks, (int)(ptMarks.X * scale), (int)(ptMarks.Y * scale), _bmpMarks.Width * scale, _bmpMarks.Height * scale);

		}

		#endregion

		#region Methods

		/// <summary>
		/// Define the physical value to be displayed on the indicator
		/// </summary>
		/// <param name="aircraftTurnRate">The aircraft turn rate in °deg per minutes</param>
		/// <param name="aircraftTurnQuality">The aircraft turn quality</param>
		public void SetTurnCoordinatorParameters(float aircraftTurnRate, float aircraftTurnQuality)
		{
			_turnRate = aircraftTurnRate;
			_turnQuality = aircraftTurnQuality;

			Refresh();
		}

		#endregion
	}
}
