using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAN;
using LibGrilleDeJeu;

namespace RubY
{
    public partial class Form1 : Form
    {
        private Cube cube;

        private Grille grid;

        private Dictionary<string, List<int>> plane = new Dictionary<string, List<int>> {
            {
                "UP",
                new List<int> {0,3}
            },
            {
                "DOWN",
                new List<int> {6,3}
            },
            {
                "LEFT",
                new List<int> {3,0}
            },
            {
                "RIGHT",
                new List<int> {3,6}
            },
            {
                "FRONT",
                new List<int> {3,3}
            },
            {
                "BACK",
                new List<int> {3,9}
            },
        };

        private List<List<int>> startErase = new List<List<int>>()
        {
            new List<int> {0, 0},
            new List<int> {0, 6},
            new List<int> {6,0},
            new List<int> {6,6},
            new List<int> {9,0},
            new List<int> {9,6}
        };

        private System.Drawing.Color[] Colors = new System.Drawing.Color[]
        {
            System.Drawing.Color.White,
            System.Drawing.Color.Green,
            System.Drawing.Color.Orange,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
        };

        public Form1()
        {
            InitializeComponent();
            Case.Controls = this.Controls;
            Case.Location = new Point(30, 30);
            grid = new Grille(12, 9, TypeCase.Button, System.Drawing.Color.White, imageList1);
            grid.UserInteraction += ActionSurLaCase;

            foreach(List<int> list in startErase)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        grid.getCase(list[0] + i, list[1] + j).CaseType = TypeCase.PictureBox;
                    }
                }
            }
            NewCube();
        }

        private void ActionSurLaCase(Interaction interaction, TypeCase typeCase, Case sender, string name) {}

        private void Update(string face, int x, int y, System.Drawing.Color new_val)
        {
            grid.getCase(plane[face][1] + x, plane[face][0] + y).BackColor = new_val;
        }

        private int[][] Rotate90(int[][] arr)
        {
            int[][] rt = new int[][] {
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rt[j][2 - i] = arr[i][j];
                }
            }
            return rt;
        }

        private int[][] Rotate270(int[][] arr)
        {
            return Rotate90(Rotate90(Rotate90(arr)));
        }

        private int[][] VMirror(int[][] arr)
        {
            int[][] rt = new int[][] {
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rt[2 - i][j] = arr[i][j];
                }
            }
            return rt;
        }

        private int[][] HMirror(int[][] arr)
        {
            int[][] rt = new int[][] {
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 },
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rt[i][2 - j] = arr[i][j];
                }
            }
            return rt;
        }

        private int[][] UTransform(int[][] arr)
        {
            return HMirror(Rotate90(arr));
        }
        private int[][] FTransform(int[][] arr)
        {
            return HMirror(Rotate90(arr));
        }
        private int[][] LTransform(int[][] arr)
        {
            return VMirror(Rotate270(arr));
        }
        private int[][] BTransform(int[][] arr)
        {
            return Rotate270(arr);
        }
        private int[][] RTransform(int[][] arr)
        {
            return Rotate270(arr);
        }
        private int[][] DTransform(int[][] arr)
        {
            return Rotate90(arr);
        }

        private void Apply()
        {
            int[][] up = UTransform(cube.Face('U'));
            int[][] front = FTransform(cube.Face('F'));
            int[][] left = LTransform(cube.Face('L'));
            int[][] back = BTransform(cube.Face('B'));
            int[][] right = RTransform(cube.Face('R'));
            int[][] down = DTransform(cube.Face('D'));

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("UP", i, j, Colors[up[i][j]]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("FRONT", i, j, Colors[front[i][j]]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("LEFT", i, j, Colors[left[i][j]]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("BACK", i, j, Colors[back[i][j]]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("RIGHT", i, j, Colors[right[i][j]]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Update("DOWN", i, j, Colors[down[i][j]]);
                }
            }
        }
        
        private void NewCube()
        {
            this.cube = new Cube();
            Apply();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            string move = ((Button)sender).Text;
            this.cube.Rotate(move);
            Apply();
        }

        private void btnScramble_Click(object sender, EventArgs e)
        {
            NewCube();

            string[][] moves = new string[][] {
                new string[] {"R", "R'", "R2"},
                new string[] {"L", "L'", "L2"},
                new string[] {"U", "U'", "U2"},
                new string[] {"D", "D'", "D2"},
                new string[] {"F", "F'", "F2"},
                new string[] {"B", "B'", "B2"},
            };

            int last = -1;
            int move = -1;

            lblScramble.Text = "";

            for (int i = 0; i < 20; i++)
            {
                while (move == last)
                {
                    Random rnd = new Random();
                    move = rnd.Next(6);
                }
                last = move;
                Random rnd_ = new Random();
                string mv = moves[move][rnd_.Next(3)];
                lblScramble.Text += mv + " ";
                cube.Rotate(mv);
            }
            Apply();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            NewCube();
            lblScramble.Text = "Pressez Mélanger pour afficher un mélange.";
        }

        private int t = 0;
        private List<string> moves = new List<string>();
        private System.Windows.Forms.Timer timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(t >= this.moves.Count - 1) timer.Stop();
            this.cube.Rotate(moves[t]);
            Apply();
            t++;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            this.cube.Solve();
            MessageBox.Show(this.cube.solution);
            this.moves = new List<string>(this.cube.moves);

            timer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };

            timer.Tick += Timer_Tick;
            timer.Start();

            lblScramble.Text = "Pressez Mélanger pour afficher un mélange.";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            sfdExport.Filter = "JSON File|*.json";
            sfdExport.ShowDialog();
        }

        private void ofdImport_FileOk(object sender, CancelEventArgs e)
        {
            this.cube.Import(ofdImport.FileName);
            Apply();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ofdImport.Filter = "JSON File|*.json";
            ofdImport.FileName = "";
            ofdImport.ShowDialog();
        }

        private void sfdExport_FileOk(object sender, CancelEventArgs e)
        {
            this.cube.Export(sfdExport.FileName);
        }
    }
}
