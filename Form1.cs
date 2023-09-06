﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Fill();
        }

        private void ActionSurLaCase(Interaction interaction, TypeCase typeCase, Case sender, string name) {}

        private void Update(string face, int x, int y, System.Drawing.Color new_val)
        {
            grid.getCase(plane[face][1] + x, plane[face][0] + y).BackColor = new_val;
        }

        private void Apply()
        {
            int[][] up = cube.Face('U');
            int[][] front = cube.Face('F');
            int[][] left = cube.Face('L');
            int[][] back = cube.Face('B');
            int[][] right = cube.Face('R');
            int[][] down = cube.Face('D');

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
        
        private void Fill()
        {
            this.cube = new Cube();
            Apply();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            this.cube.Rotate(((Button)sender).Text);
            Apply();
        }

        private void btnScramble_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Fill();
        }
    }
}
