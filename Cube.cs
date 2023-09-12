﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAN
{
    public static class Color
    {
        public const int NONE = -1;
        public const int WHITE = 0;
        public const int GREEN = 1;
        public const int ORANGE = 2;
        public const int BLUE = 3;
        public const int RED = 4;
        public const int YELLOW = 5;
    }

    #region Algorithms
    public static class Algorithms
    {
        public static Dictionary<string, string> OLL = new Dictionary<string, string>
        {
            {
                "OCLL6", "R U2 R' U' R U' R'"
            },
            {
                "OCLL1", "R U2 R' U' R U R' U' R U' R'"
            },
            {
                "OCLL4", "r U R' U' r' F R F'"
            },
            {
                "OCLL3", "R2 D R' U2 R D' R' U2 R'"
            },
            {
                "OCLL7", "R U R' U R U2' R'"
            },
            {
                "OCLL2", "R U2' R2' U' R2 U' R2' U2' R"
            },
            {
                "OCLL5", "y F' r U R' U' r' F R"
            },
            {
                "T1", "R U R' U' R' F R F'"
            },
            {
                "T2", "F R U R' U' F'"
            },
            {
                "S1", "r' U2' R U R' U r"
            },
            {
                "S2", "r U2 R' U' R U' r'"
            },
            {
                "C1", "r U2 R' U' R U' r'"
            }
        };
    }
    #endregion

    #region MiniCube
    public class MiniCube : ICloneable {
        public int U;
        public int D;
        public int F;
        public int B;
        public int L;
        public int R;

        public MiniCube(int U, int D, int F, int B, int L, int R) {
            this.U = U;
            this.D = D;
            this.F = F;
            this.B = B;
            this.L = L;
            this.R = R;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Turn(string way)
        {
            int tmp;
            switch (way)
            {
                case "U":
                case "D'":
                    tmp = this.F;
                    this.F = this.R;
                    this.R = this.B;
                    this.B = this.L;
                    this.L = tmp;
                    break;
                case "D":
                case "U'":
                    tmp = this.F;
                    this.F = this.L;
                    this.L = this.B;
                    this.B = this.R;
                    this.R = tmp;
                    break;
                case "F":
                case "B'":
                    tmp = this.U;
                    this.U = this.L;
                    this.L = this.D;
                    this.D = this.R;
                    this.R = tmp;
                    break;
                case "B":
                case "F'":
                    tmp = this.U;
                    this.U = this.R;
                    this.R = this.D;
                    this.D = this.L;
                    this.L = tmp;
                    break;
                case "R":
                case "L'":
                    tmp = this.U;
                    this.U = this.F;
                    this.F = this.D;
                    this.D = this.B;
                    this.B = tmp;
                    break;
                case "L":
                case "R'":
                    tmp = this.U;
                    this.U = this.B;
                    this.B = this.D;
                    this.D = this.F;
                    this.F = tmp;
                    break;
            }
        }

        public List<int> Colors()
        {
            List<int> ints = new List<int>();
            if(this.U != Color.NONE) ints.Add(this.U);
            if(this.D != Color.NONE) ints.Add(this.D);
            if(this.F != Color.NONE) ints.Add(this.F);
            if(this.B != Color.NONE) ints.Add(this.B);
            if(this.R != Color.NONE) ints.Add(this.R);
            if(this.L != Color.NONE) ints.Add(this.L);
            return ints;
        }
    }
    #endregion

    public class Cube
    {
        public List<string> moves = new List<string> { };
        #region Cube_Declaration
        private MiniCube[][][] cube = new MiniCube[][][]
        {
            new MiniCube[][]
            {
                new MiniCube[]
                {
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.BLUE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.BLUE, Color.NONE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.BLUE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.WHITE, Color.NONE, Color.GREEN, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.GREEN, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.WHITE, Color.NONE, Color.GREEN, Color.NONE, Color.NONE, Color.RED),
                },
            },
            new MiniCube[][]
            {
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.BLUE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.BLUE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.BLUE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.NONE, Color.GREEN, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.GREEN, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.NONE, Color.GREEN, Color.NONE, Color.NONE, Color.RED),
                },
            },
            new MiniCube[][]
            {
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.BLUE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.BLUE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.BLUE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.NONE, Color.NONE, Color.NONE, Color.RED),
                },
                new MiniCube[]
                {
                    new MiniCube(Color.NONE, Color.YELLOW, Color.GREEN, Color.NONE, Color.ORANGE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.GREEN, Color.NONE, Color.NONE, Color.NONE),
                    new MiniCube(Color.NONE, Color.YELLOW, Color.GREEN, Color.NONE, Color.NONE, Color.RED),
                },
            },
        };
        #endregion

        #region Solving

        int[] WhiteCenterPos = new int[3] { 0, 1, 1 };

        int cross_color = Color.NONE;
        int fcolor = Color.NONE;

        private bool IsCrossDone()
        {
            cross_color = this.cube[2][1][1].D;
            fcolor = this.cube[1][2][1].F;
            return (this.cube[2][0][1].D == cross_color) && (this.cube[2][0][1].B == this.cube[1][0][1].B) &&
                (this.cube[2][1][0].D == cross_color) && (this.cube[2][1][0].L == this.cube[1][1][0].L) &&
                (this.cube[2][2][1].D == cross_color) && (this.cube[2][2][1].F == this.cube[1][2][1].F) &&
                (this.cube[2][1][2].D == cross_color) && (this.cube[2][1][2].R == this.cube[1][1][2].R);
        }

        private int[] SideColors()
        {
            int[] sideColors = new int[4] { 0, 0, 0, 0 };
            int tmp = 0;
            for(int i = 0; i < 6; i++) {
                if(i != cross_color && i != 5 - cross_color) sideColors[tmp++] = i;
            }
            return sideColors;
        }

        int[] findEdge(int color1, int color2)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        if(i == 1 || j == 1 || k == 1)
                        {
                            List<int> clist = this.cube[i][j][k].Colors();
                            if (clist.Contains(color1) && clist.Contains(color2)) return new int[] { i, j, k };
                        }
                    }
                }
            }
            return new int[] { -1, -1, -1 };
        }

        int[] findCorner(int color1, int color2, int color3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i != 1 && j != 1 && k != 1)
                        {
                            List<int> clist = this.cube[i][j][k].Colors();
                            if (clist.Contains(color1) && clist.Contains(color2) && clist.Contains(color3)) return new int[] { i, j, k };
                        }
                    }
                }
            }
            return new int[] { -1, -1, -1 };
        }

        public void MakeCross()
        {
            if(IsCrossDone()) return;

            foreach(int color in SideColors())
            {
                int[] cp = findEdge(cross_color, color);
                MessageBox.Show($"{cp[0]}, {cp[1]}, {cp[2]}");
            }
        }

        public void Solve()
        {
            MakeCross();
        }
        #endregion

        #region U_D_Layers_Move

        private void TopClockwise(int layer)
        {
            this.cube[layer] = new MiniCube[][]
            {
                new MiniCube[]
                {
                    this.cube[layer][2][0].Clone() as MiniCube,
                    this.cube[layer][1][0].Clone() as MiniCube,
                    this.cube[layer][0][0].Clone() as MiniCube,
                },
                new MiniCube[]
                {
                    this.cube[layer][2][1].Clone() as MiniCube,
                    this.cube[layer][1][1].Clone() as MiniCube,
                    this.cube[layer][0][1].Clone() as MiniCube,
                },
                new MiniCube[]
                {
                    this.cube[layer][2][2].Clone() as MiniCube,
                    this.cube[layer][1][2].Clone() as MiniCube,
                    this.cube[layer][0][2].Clone() as MiniCube,
                },
            };
            for (int i = 0; i < this.cube[0].Length; i++)
            {
                for (int j = 0; j < this.cube[0][i].Length; j++)
                {
                    this.cube[layer][i][j].Turn("U");
                }
            }
        }

        private void TopCounterClockwise(int layer)
        {
            this.cube[layer] = new MiniCube[][]
            {
                new MiniCube[]
                {
                    this.cube[layer][0][2].Clone() as MiniCube,
                    this.cube[layer][1][2].Clone() as MiniCube,
                    this.cube[layer][2][2].Clone() as MiniCube,
                },
                new MiniCube[]
                {
                    this.cube[layer][0][1].Clone() as MiniCube,
                    this.cube[layer][1][1].Clone() as MiniCube,
                    this.cube[layer][2][1].Clone() as MiniCube,
                },
                new MiniCube[]
                {
                    this.cube[layer][0][0].Clone() as MiniCube,
                    this.cube[layer][1][0].Clone() as MiniCube,
                    this.cube[layer][2][0].Clone() as MiniCube,
                },
            };
            for (int i = 0; i < this.cube[0].Length; i++)
            {
                for (int j = 0; j < this.cube[0][i].Length; j++)
                {
                    this.cube[layer][i][j].Turn("U'");
                }
            }
        }

        private void U() {
            this.TopClockwise(0);
        }
        private void Uprime() {
            this.TopCounterClockwise(0);
        }
        private void U2() {
            this.U();
            this.U();
        }
        private void u() {
            this.U();
            this.Eprime();
        }
        private void D() {
            this.TopCounterClockwise(2);
        }
        private void Dprime() {
            this.TopClockwise(2);
        }
        private void D2() {
            this.D();
            this.D();
        }
        private void d() {
            this.D();
            this.E();
        }
        #endregion

        #region F_B_Layers_Move
        private void F() {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    this.cube[0][0],
                    this.cube[0][1],
                    new MiniCube[]{
                        this.cube[2][2][0],
                        this.cube[1][2][0],
                        this.cube[0][2][0],
                    },
                },
                new MiniCube[][]
                {
                    this.cube[1][0],
                    this.cube[1][1],
                    new MiniCube[]{
                        this.cube[2][2][1],
                        this.cube[1][2][1],
                        this.cube[0][2][1],
                    },
                },
                new MiniCube[][]
                {
                    this.cube[2][0],
                    this.cube[2][1],
                    new MiniCube[]{
                        this.cube[2][2][2],
                        this.cube[1][2][2],
                        this.cube[0][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][2].Length; j++)
                {
                    this.cube[i][2][j].Turn("F");
                }
            }
        }
        private void Fprime() {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    this.cube[0][0],
                    this.cube[0][1],
                    new MiniCube[]{
                        this.cube[0][2][2],
                        this.cube[1][2][2],
                        this.cube[2][2][2],
                    },
                },
                new MiniCube[][]
                {
                    this.cube[1][0],
                    this.cube[1][1],
                    new MiniCube[]{
                        this.cube[0][2][1],
                        this.cube[1][2][1],
                        this.cube[2][2][1],
                    },
                },
                new MiniCube[][]
                {
                    this.cube[2][0],
                    this.cube[2][1],
                    new MiniCube[]{
                        this.cube[0][2][0],
                        this.cube[1][2][0],
                        this.cube[2][2][0],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][2].Length; j++)
                {
                    this.cube[i][2][j].Turn("F'");
                }
            }
        }
        private void F2() {
            this.F();
            this.F();
        }
        private void f() {
            this.F();
            this.S();
        }
        private void B() {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[0][0][2],
                        this.cube[1][0][2],
                        this.cube[2][0][2],
                    },
                    this.cube[0][1],
                    this.cube[0][2],
                },
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[0][0][1],
                        this.cube[1][0][1],
                        this.cube[2][0][1],
                    },
                    this.cube[1][1],
                    this.cube[1][2],
                },
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[0][0][0],
                        this.cube[1][0][0],
                        this.cube[2][0][0],
                    },
                    this.cube[2][1],
                    this.cube[2][2],
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][0].Length; j++)
                {
                    this.cube[i][0][j].Turn("B");
                }
            }
        }
        private void Bprime() {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[2][0][0],
                        this.cube[1][0][0],
                        this.cube[0][0][0],
                    },
                    this.cube[0][1],
                    this.cube[0][2],
                },
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[2][0][1],
                        this.cube[1][0][1],
                        this.cube[0][0][1],
                    },
                    this.cube[1][1],
                    this.cube[1][2],
                },
                new MiniCube[][]
                {
                    new MiniCube[]{
                        this.cube[2][0][2],
                        this.cube[1][0][2],
                        this.cube[0][0][2],
                    },
                    this.cube[2][1],
                    this.cube[2][2],
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][0].Length; j++)
                {
                    this.cube[i][0][j].Turn("F");
                }
            }
        }
        private void B2() {
            this.B();
            this.B();
        }
        private void b() {
            this.B();
            this.Sprime();
        }
        #endregion

        #region R_L_Layers_Move
        private void R() {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[0][0][1],
                        this.cube[0][2][2],
                    },
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[0][1][1],
                        this.cube[1][2][2],
                    },
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[0][2][1],
                        this.cube[2][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[1][0][1],
                        this.cube[0][1][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[1][2][1],
                        this.cube[2][1][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[2][0][1],
                        this.cube[0][0][2],
                    },
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[2][1][1],
                        this.cube[1][0][2],
                    },
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[2][2][1],
                        this.cube[2][0][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][2].Turn("R");
                }
            }
        }
        private void Rprime() {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[0][0][1],
                        this.cube[2][0][2],
                    },
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[0][1][1],
                        this.cube[1][0][2],
                    },
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[0][2][1],
                        this.cube[0][0][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[1][0][1],
                        this.cube[2][1][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[1][2][1],
                        this.cube[0][1][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[2][0][1],
                        this.cube[2][2][2],
                    },
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[2][1][1],
                        this.cube[1][2][2],
                    },
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[2][2][1],
                        this.cube[0][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][2].Turn("R'");
                }
            }
        }
        private void R2() {
            this.R();
            this.R();
        }
        private void r() {
            this.R();
            this.Mprime();
        }
        private void L() {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[0][0][1],
                        this.cube[0][0][2],
                    },
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[0][1][1],
                        this.cube[0][1][2],
                    },
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[0][2][1],
                        this.cube[0][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[1][0][1],
                        this.cube[1][0][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[1][2][1],
                        this.cube[1][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[2][0][1],
                        this.cube[2][0][2],
                    },
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[2][1][1],
                        this.cube[2][1][2],
                    },
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[2][2][1],
                        this.cube[2][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][0].Turn("L");
                }
            }
        }
        private void Lprime() {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[0][0][1],
                        this.cube[0][0][2],
                    },
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[0][1][1],
                        this.cube[0][1][2],
                    },
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[0][2][1],
                        this.cube[0][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[1][0][1],
                        this.cube[1][0][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[1][2][1],
                        this.cube[1][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[2][0][1],
                        this.cube[2][0][2],
                    },
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[2][1][1],
                        this.cube[2][1][2],
                    },
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[2][2][1],
                        this.cube[2][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][0].Turn("L'");
                }
            }
        }
        private void L2() {
            this.L();
            this.L();
        }
        private void l() {
            this.L();
            this.M();
        }
        #endregion

        #region M_E_S_Layers_Move
        private void M()
        {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[2][0][1],
                        this.cube[0][0][2],
                    },
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[1][0][1],
                        this.cube[0][1][2],
                    },
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[0][0][1],
                        this.cube[0][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[2][1][1],
                        this.cube[1][0][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[0][1][1],
                        this.cube[1][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[2][2][1],
                        this.cube[2][0][2],
                    },
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[1][2][1],
                        this.cube[2][1][2],
                    },
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[0][2][1],
                        this.cube[2][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][1].Turn("L");
                }
            }
        }
        private void Mprime()
        {
            this.cube = new MiniCube[][][]
            {
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[0][0][0],
                        this.cube[0][2][1],
                        this.cube[0][0][2],
                    },
                    new MiniCube[] {
                        this.cube[0][1][0],
                        this.cube[1][2][1],
                        this.cube[0][1][2],
                    },
                    new MiniCube[] {
                        this.cube[0][2][0],
                        this.cube[2][2][1],
                        this.cube[0][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[1][0][0],
                        this.cube[0][1][1],
                        this.cube[1][0][2],
                    },
                    this.cube[1][1],
                    new MiniCube[] {
                        this.cube[1][2][0],
                        this.cube[2][1][1],
                        this.cube[1][2][2],
                    },
                },
                new MiniCube[][]
                {
                    new MiniCube[] {
                        this.cube[2][0][0],
                        this.cube[0][0][1],
                        this.cube[2][0][2],
                    },
                    new MiniCube[] {
                        this.cube[2][1][0],
                        this.cube[1][0][1],
                        this.cube[2][1][2],
                    },
                    new MiniCube[] {
                        this.cube[2][2][0],
                        this.cube[2][0][1],
                        this.cube[2][2][2],
                    },
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i].Length; j++)
                {
                    this.cube[i][j][1].Turn("L'");
                }
            }
        }
        private void M2()
        {
            this.M();
            this.M();
        }
        private void E()
        {
            TopCounterClockwise(1);
        }
        private void Eprime()
        {
            TopClockwise(1);
        }
        private void E2()
        {
            this.E();
            this.E();
        }
        private void S()
        {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    this.cube[0][0],
                    new MiniCube[]{
                        this.cube[2][1][0],
                        this.cube[1][1][0],
                        this.cube[0][1][0],
                    },
                    this.cube[0][2],
                },
                new MiniCube[][]
                {
                    this.cube[1][0],
                    new MiniCube[]{
                        this.cube[2][1][1],
                        this.cube[1][1][1],
                        this.cube[0][1][1],
                    },
                    this.cube[1][2],
                },
                new MiniCube[][]
                {
                    this.cube[2][0],
                    new MiniCube[]{
                        this.cube[2][1][2],
                        this.cube[1][1][2],
                        this.cube[0][1][2],
                    },
                    this.cube[2][2],
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][2].Length; j++)
                {
                    this.cube[i][1][j].Turn("F");
                }
            }
        }
        private void Sprime()
        {
            this.cube = new MiniCube[][][] {
                new MiniCube[][]
                {
                    this.cube[0][0],
                    new MiniCube[]{
                        this.cube[0][1][2],
                        this.cube[1][1][2],
                        this.cube[2][1][2],
                    },
                    this.cube[0][2],
                },
                new MiniCube[][]
                {
                    this.cube[1][0],
                    new MiniCube[]{
                        this.cube[0][1][1],
                        this.cube[1][1][1],
                        this.cube[2][1][1],
                    },
                    this.cube[1][2],
                },
                new MiniCube[][]
                {
                    this.cube[2][0],
                    new MiniCube[]{
                        this.cube[0][1][0],
                        this.cube[1][1][0],
                        this.cube[2][1][0],
                    },
                    this.cube[2][2],
                },
            };
            for (int i = 0; i < this.cube.Length; i++)
            {
                for (int j = 0; j < this.cube[i][1].Length; j++)
                {
                    this.cube[i][1][j].Turn("F'");
                }
            }
        }
        private void S2()
        {
            this.S();
            this.S();
        }
        #endregion

        #region x_y_z_Move
        private void x() {
            this.R();
            this.Mprime();
            this.Lprime();
        }
        private void xprime() {
            this.Rprime();
            this.M();
            this.L();
        }
        private void x2() {
            this.x();
            this.x();
        }
        private void y() {
            this.U();
            this.Eprime();
            this.Dprime();
        }
        private void yprime() {
            this.Uprime();
            this.E();
            this.D();
        }
        private void y2() {
            this.y();
            this.y();
        }
        private void z() {
            this.F();
            this.S();
            this.Bprime();
        }
        private void zprime() {
            this.Fprime();
            this.Sprime();
            this.B();
        }
        private void z2() {
            this.z();
            this.z();
        }
        #endregion

        #region RotateFn
        public void Rotate(string way)
        {
            switch(way)
            {
                case "U":
                    U();
                    break;
                case "U'":
                    Uprime();
                    break;
                case "U2":
                    U2();
                    break;
                case "u":
                    u();
                    break;
                case "D":
                    D();
                    break;
                case "D'":
                    Dprime();
                    break;
                case "D2":
                    D2();
                    break;
                case "d":
                    d();
                    break;
                case "F":
                    F();
                    break;
                case "F'":
                    Fprime();
                    break;
                case "F2":
                    F2();
                    break;
                case "f":
                    f();
                    break;
                case "B":
                    B();
                    break;
                case "B'":
                    Bprime();
                    break;
                case "B2":
                    B2();
                    break;
                case "b":
                    b();
                    break;
                case "R":
                    R();
                    break;
                case "R'":
                    Rprime();
                    break;
                case "R2":
                    R2();
                    break;
                case "r":
                    r();
                    break;
                case "L":
                    L();
                    break;
                case "L'":
                    Lprime();
                    break;
                case "L2":
                    L2();
                    break;
                case "l":
                    l();
                    break;
                case "x":
                    x();
                    break;
                case "y":
                    y();
                    break;
                case "z":
                    z();
                    break;
                case "x'":
                    xprime();
                    break;
                case "y'":
                    yprime();
                    break;
                case "z'":
                    zprime();
                    break;
                case "x2":
                    x2();
                    break;
                case "y2":
                    y2();
                    break;
                case "z2":
                    z2();
                    break;
                case "M":
                    M();
                    break;
                case "M'":
                    Mprime();
                    break;
                case "M2":
                    M2();
                    break;
                case "E":
                    E();
                    break;
                case "E'":
                    Eprime();
                    break;
                case "E2":
                    E2();
                    break;
                case "S":
                    S();
                    break;
                case "S'":
                    Sprime();
                    break;
                case "S2":
                    S2();
                    break;
            }
        }
        #endregion

        #region Face_Fn
        public int[][] Face(char face)
        {
            int[][] f = new int[3][] {
                new int[3] {0, 0, 0},
                new int[3] {0, 0, 0},
                new int[3] {0, 0, 0},
            };

            MiniCube[][] mc;
            switch (face)
            {
                case 'U':
                    for(int i = 0; i < this.cube[0].Length; i++)
                    {
                        for (int j = 0; j < this.cube[0][i].Length; j++)
                        {
                            f[i][j] = this.cube[0][i][j].U;
                        }
                    }
                    return f;
                case 'D':
                    for (int i = 0; i < this.cube[2].Length; i++)
                    {
                        for (int j = 0; j < this.cube[2][i].Length; j++)
                        {
                            f[i][j] = this.cube[2][i][j].D;
                        }
                    }
                    return f;
                case 'F':
                    mc = new MiniCube[][] {
                        this.cube[0][2],
                        this.cube[1][2],
                        this.cube[2][2],
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        for(int j = 0; j < mc[i].Length; j++)
                        {
                            f[i][j] = mc[i][j].F;
                        }
                    }
                    return f;
                case 'B':
                    mc = new MiniCube[][] {
                        this.cube[0][0],
                        this.cube[1][0],
                        this.cube[2][0],
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        for (int j = 0; j < mc[i].Length; j++)
                        {
                            f[i][j] = mc[i][j].B;
                        }
                    }
                    return f;
                case 'L':
                    mc = new MiniCube[][] {
                        new MiniCube[] {
                            this.cube[0][0][0],
                            this.cube[0][1][0],
                            this.cube[0][2][0],
                        },
                        new MiniCube[] {
                            this.cube[1][0][0],
                            this.cube[1][1][0],
                            this.cube[1][2][0],
                        },
                        new MiniCube[] {
                            this.cube[2][0][0],
                            this.cube[2][1][0],
                            this.cube[2][2][0],
                        },
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        for (int j = 0; j < mc[i].Length; j++)
                        {
                            f[i][j] = mc[i][j].L;
                        }
                    }
                    return f;
                case 'R':
                    mc = new MiniCube[][] {
                        new MiniCube[] {
                            this.cube[0][0][2],
                            this.cube[0][1][2],
                            this.cube[0][2][2],
                        },
                        new MiniCube[] {
                            this.cube[1][0][2],
                            this.cube[1][1][2],
                            this.cube[1][2][2],
                        },
                        new MiniCube[] {
                            this.cube[2][0][2],
                            this.cube[2][1][2],
                            this.cube[2][2][2],
                        },
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        for (int j = 0; j < mc[i].Length; j++)
                        {
                            f[i][j] = mc[i][j].R;
                        }
                    }
                    return f;
                default:
                    return f;
            }
        }
        #endregion
    }
}
