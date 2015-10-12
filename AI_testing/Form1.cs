using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_testing
{
    public partial class Form1 : Form
    {
        const int rowcount = 101;
        const int colcount = 101;
        int[,] inputEnvironment = new int[rowcount, colcount];
        
        public Form1()
        {
            Random r = new Random();
            InitializeComponent();
            Initialize();
            DepthFirstSearch dfs = new DepthFirstSearch();
            //inputEnvironment = new int[,] { { 1, 1, 1, 1, 1 }, { 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 1 }, { 1, 1, 0, 0, 1 }, { 1, 1, 1, 0, 1 } };
            inputEnvironment= dfs.FillArrayUsingDepthFirstSearch(inputEnvironment);
            List<Tuple<int,int>> inputKVP = new List<Tuple<int,int>>();
            MapToDGV();


            //inputKVP.Add(new Tuple<int, int>(1, 5));
            //inputKVP.Add(new Tuple<int, int>(2, 10));
            //inputKVP.Add(new Tuple<int, int>(3, 20));
            //BinaryHeap<int,int> pq = new BinaryHeap<int,int>(inputKVP);

            //List<int> output= pq.printHeap();

            Tuple<int, int> startTuple = new Tuple<int, int>(r.Next(0, rowcount-1), r.Next(0, colcount-1));
            Tuple<int, int> goalTuple = new Tuple<int, int>(r.Next(0, rowcount -1), r.Next(0, colcount-1));

            datagridview_array.Rows[startTuple.Item1].Cells[startTuple.Item2].Style.BackColor = Color.Green;
            datagridview_array.Rows[goalTuple.Item1].Cells[goalTuple.Item2].Style.BackColor = Color.Red;


            RepeatedForward rf = new RepeatedForward();
            rf.RepeatedForwardAStar(inputEnvironment, startTuple, goalTuple);
            //rf.RepeatedForwardAStar(inputEnvironment, new Tuple<int, int>(4, 2), new Tuple<int, int>(4, 4));


            Console.WriteLine("Repeated Forward A* is done"); ;
            AdaptiveAStar aa = new AdaptiveAStar();
            aa.AdaptiveAStarAlgorithm(inputEnvironment, startTuple, goalTuple);
            //aa.AdaptiveAStarAlgorithm(inputEnvironment, new Tuple<int, int>(4, 2), new Tuple<int, int>(4, 4));

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Initialize()
        {
            for(int i=0; i<rowcount;i++)
            {
                for(int j=0; j<colcount;j++)
                {
                    inputEnvironment[i,j] = -1;
                }
            }
        }
        private void MapToDGV(){

            for (int rowIndex = 0; rowIndex < rowcount; ++rowIndex)
            {
                var row = new DataGridViewRow();                
                for (int columnIndex = 0; columnIndex < colcount; ++columnIndex)
                {
                    //var col = new DataGridViewColumn();
                    if (datagridview_array.Columns == null ||datagridview_array.Columns.Count < columnIndex+1)
                    {
                        var col = new DataGridViewColumn();
                        datagridview_array.Columns.Add(col);
                    }

                    var cell = new DataGridViewTextBoxCell();
                    //col.CellTemplate = cell;

                    cell.Value = inputEnvironment[rowIndex, columnIndex];

                    row.Cells.Add(cell);

                    //datagridview_array.Columns.Add(col);

                }
                

                datagridview_array.Rows.Add(row);
            }

        }
    

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
