using Infragistics.UltraChart.Core.Primitives;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UltraChart_Symbol
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UltraChart1.ChartType = ChartType.LineChart;
            UltraChart1.DataSource = getLineData();
            UltraChart1.DataBind();

            UltraChart1.Axis.Y.RangeType = AxisRangeType.Custom;
            UltraChart1.Axis.Y.RangeMin = 0;
            UltraChart1.Axis.Y.RangeMax = 150;

            LineAppearance lineApp1 = new LineAppearance();
            lineApp1.IconAppearance.Icon = SymbolIcon.None;
            UltraChart1.LineChart.LineAppearances.Add(lineApp1);
        }

        private DataTable getLineData()
        {
            DataTable table = new DataTable();

            table.Columns.Add("StockName", typeof(string));
            table.Columns.Add("Monday", typeof(Int32));
            table.Columns.Add("Tuesday", typeof(Int32));
            table.Columns.Add("Wednesday", typeof(Int32));
            table.Columns.Add("Thursday", typeof(Int32));
            table.Columns.Add("Friday", typeof(Int32));

            table.Rows.Add(new object[] { "IBM", 111, 96, 85, 125, 65 });
            table.Rows.Add(new object[] { "MSN", 75, 23, 15, 75, 100 });
            table.Rows.Add(new object[] { "MCI", 55, 63, 84, 95, 80 });
            table.Rows.Add(new object[] { "ATT", 12, 12, 40, 60, 50 });
            table.Rows.Add(new object[] { "VER", 69, 95, 110, 98, 123 });

            return table;
        }

        protected void UltraChart1_FillSceneGraph(object sender, Infragistics.UltraChart.Shared.Events.FillSceneGraphEventArgs e)
        {
            PrimitiveCollection symbols = new PrimitiveCollection();
            foreach (Primitive p in e.SceneGraph)
            {
                Polyline polyline = p as Polyline;
                if (polyline != null)
                {
                    foreach (DataPoint dataPoint in polyline.points)
                    {
                        if ((dataPoint.DataPoint as NumericDataPoint).Value >= 100)
                        {
                            Symbol symbol = new Symbol();
                            symbol.icon = SymbolIcon.Circle;
                            symbol.iconSize = SymbolIconSize.Medium;
                            symbol.PE.Fill = Color.Red;
                            symbol.PE.Stroke = Color.Red;
                            symbol.point = dataPoint.point;
                            symbols.Add(symbol);
                        }
                    }
                }
            }

            e.SceneGraph.AddRange(symbols.ToArray());
        }
    }
}