using System;
using System.Collections.Generic;
using System.Drawing;
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
                "C1", "R U R2' U' R' F R U R U' F'"
            },
            {
                "C1", "R' U' R' F R F' U R"
            },
            {
                "W1", "R' U' R U' R' U R U l U' R' U x"
            },
            {
                "W2", "R U R' U R U' R' U' R' F R F'"
            },
            {
                "E1", "r U R' U' M U R U' R'"
            },
            {
                "E2", "R U R' U' M' U R U' r'"
            },
            {
                "P1", "R' U' F U R U' R' F' R"
            },
            {
                "P2", "R U B' U' R' U R B R'"
            },
            {
                "P3", "y R' U' F' U F R"
            },
            {
                "P4", "f R U R' U' f'"
            },
            {
                "I1", "f R U R' U' R U R' U' f'"
            },
            {
                "I2", "R' U' R U' R' U y' R' U R B"
            },
            {
                "I3", "y R' F R U R U' R2' F' R2 U' R' U R U R'"
            },
            {
                "I4", "r' U' r U' R' U R U' R' U R r' U r"
            },
            {
                "F1", "R U R' U' R' F R2 U R' U' F'"
            },
            {
                "F2", "R U R' U R' F R F' R U2' R'"
            },
            {
                "F3", "R U2' R2' F R F' R U2' R'"
            },
            {
                "F4", "F R U' R' U' R U R' F'"
            },
            {
                "K1", "r U' r' U' r U r' y' R' U R"
            },
            {
                "K2", "R' F R U R' F' R F U' F'"
            },
            {
                "K3", "r' U' r R' U' R U r' U r"
            },
            {
                "K4", "r U r' R U R' U' r U' r'"
            },
            {
                "A1", "y R U R' U' R U' R' F' U' F R U R'"
            },
            {
                "A2", "y' F U R U2 R' U' R U2 R' U' F'"
            },
            {
                "A3", "R U R' U R U2' R' F R U R' U' F'"
            },
            {
                "A4", "R' U' R U' R' U2 R F R U R' U' F'"
            },
            {
                "L1", "F' L' U' L U L' U' L U F"
            },
            {
                "L2", "F R U R' U' R U R' U' F'"
            },
            {
                "L3", "r U' r2' U r2 U r2' U' r"
            },
            {
                "L4", "r' U r2 U' r2' U' r2 U r'"
            },
            {
                "L5", "r' U' R U' R' U R U' R' U2 r"
            },
            {
                "L6", "r U R' U R U' R' U R U2' r'"
            },
            {
                "B1", "r U R' U R U2' r'"
            },
            {
                "B2", "r' U' R U' R' U2 r"
            },
            {
                "B3", "r' R2 U R' U R U2 R' U M'"
            },
            {
                "B4", "M' R' U' R U' R' U2 R U' M\r\n"
            },
            {
                "B5", "L F' L' U' L U F U' L'"
            },
            {
                "B6", "R' F R U R' U' F' U R"
            },
            {
                "O1", "R U2' R2' F R F' U2' R' F R F'"
            },
            {
                "O2", "F R U R' U' F' f R U R' U' f'"
            },
            {
                "O3", "f R U R' U' f' U' F R U R' U' F'"
            },
            {
                "O4", "f R U R' U' f' U F R U R' U' F'"
            },
            {
                "O5", "R U R' U R' F R F' U2' R' F R F'"
            },
            {
                "O6", "y R U2' R2' F R F' U2' M' U R U' r'"
            },
            {
                "O6", "M U R U R' U' M' R' F R F'"
            },
            {
                "O7", "M U R U R' U' M2' U R U' r'"
            }
        };

        public static Dictionary<string, string> PLL = new Dictionary<string, string>
        {
            {
                "Ua", "R U' R U R U R U' R' U' R2"
            },
            {
                "Ub", "R2 U R U R' U' R' U' R' U R'"
            },
            {
                "Z", "M2' U M2' U M' U2 M2' U2 M' U2"
            },
            {
                "H", "M2' U M2' U2 M2' U M2'"
            },
            {
                "Aa", "x R' U R' D2 R U' R' D2 R2 x'"
            },
            {
                "Ab", "x R2' D2 R U R' D2 R U' R x'"
            },
            {
                "E", "x' R U' R' D R U R' D' R U R' D R U' R' D' x"
            },
            {
                "Ra", "R U' R' U' R U R D R' U' R D' R' U2 R' U'"
            },
            {
                "Rb", "R' U2 R U2' R' F R U R' U' R' F' R2 U'"
            },
            {
                "Ja", "R' U L' U2 R U' R' U2 R L U'"
            },
            {
                "Jb", "R U R' F' R U R' U' R' F R2 U' R' U'"
            },
            {
                "T", "R U R' U' R' F R2 U' R' U' R U R' F'"
            },
            {
                "F", "R' U' F'R U R' U'R' F R2 U'R' U' R UR' U R"
            },
            {
                "V", "R' U R' U' y R' F' R2 U' R' U R' F R F"
            },
            {
                "Y", "F R U' R' U' R U R' F' R U R' U' R' F R F'"
            },
            {
                "Na", "R U R' U R U R' F' R U R'U' R' F R2 U' R' U2 R U' R'"
            },
            {
                "Nb", "R' U R U' R' F' U' F R U R' F R' F' R U' R"
            },
            {
                "Ga", "R2 U R' U R' U' R U' R2 D U' R' U R D' U"
            },
            {
                "Gb", "F' U' F R2 u R' U R U' R u' R2'"
            },
            {
                "Gc", "R2 U' R U' R U R' U R2 D' U R U' R' D U'"
            },
            {
                "Gd", "D' R U R' U' D R2 U' R U' R' U R' U R2 U"
            }
        };

        public static Dictionary<string, string> F2L = new Dictionary<string, string>
        {
            {
                "BI_1", "U R U' R'"
            },
            {
                "BI_2", "y' U' R' U R"
            },
            {
                "BI_3", "y' (R' U' R)"
            },
            {
                "BI_4", "(R U R')"
            },
            {
                "Case1_1", "U' (R U' R' U) y' (R' U' R)"
            },
            {
                "Case1_2", "U' (R U R' U) (R U R')"
            },
            {
                "Case1_3", "U' (R U2' R' U) y' (R' U' R)"
            },
            {
                "Case1_4", "R' U2' R2 U R2' U R"
            },
            {
                "Case1_5", "y' U (R' U R U') (R' U' R) U"
            },
            {
                "Case1_6", "U' (R U' R' U) (R U R')"
            },
            {
                "Case2_1", "(U' R U R') U2 (R U' R')"
            },
            {
                "Case2_2", "y' (U R' U' R) U2' (R' U R)"
            },
            {
                "Case2_3", "U' (R U2' R') U2 (R U' R')"
            },
            {
                "Case2_4", "y' U (R' U2 R) U2' (R' U R)"
            },
            {
                "Case3_1", "U (R U2 R') U (R U' R')"
            },
            {
                "Case3_2", "y' U' (R' U2 R) U' (R' U R)"
            },
            {
                "Case3_3", "U2 (R U R' U) (R U' R')"
            },
            {
                "Case3_4", "y' U2 (R' U' R) U' (R' U R)"
            },
            {
                "ICP_1", "y' (R' U R) U2' y (R U R')"
            },
            {
                "ICP_2", "(R U' R' U2) y' (R' U' R)"
            },
            {
                "ICP_3", "(R U2 R') U' (R U R') "
            },
            {
                "ICP_4", "y' (R' U2 R) U (R' U' R)"
            },
            {
                "ICP_5", "U (R U' R' U') (R U' R' U) (R U' R')"
            },
            {
                "ICP_6", "y' U' (R' U R U) (R' U R U') (R' U R)"
            },
            {
                "CIP_1", "U' F' (R U R' U') R' F R"
            },
            {
                "CIP_2", "U (R U' R') U' (F' U F)"
            },
            {
                "CIP_3", "(R U' R' U) (R U' R')"
            },
            {
                "CIP_4", "y' (R' U R U') (R' U R)"
            },
            {
                "CIP_5", "y' (R' U' R U) (R' U' R)"
            },
            {
                "CIP_6", "(R U R' U') (R U R')"
            },
            {
                "EIP_1", "(R U' R' U) y' (R' U R)"
            },
            {
                "EIP_2", "(U R U' R') (U R U' R') (U R U' R')"
            },
            {
                "EIP_3", "(U' R U' R') U2 (R U' R')"
            },
            {
                "EIP_4", "U (R U R') U2 (R U R')"
            },
            {
                "EIP_5", "(U' R U R') U y' (R' U' R)"
            },
            {
                "EIP_6", "U (F' U' F) U' (R U R')"
            },
            {
                "ECP_1", "(R U' R') d (R' U2 R) U2' (R' U R)"
            },
            {
                "ECP_2", "(R U' R' U') R U R' U2 (R U' R')"
            },
            {
                "ECP_3", "(R U' R' U) (R U2' R') U (R U' R')"
            },
            {
                "ECP_4", "(F' U F) U2 (R U R' U) (R U' R')"
            },
            {
                "ECP_5", "(R U R' U') (R U' R') U2 y' (R' U' R)"
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

        private int[] SideColors() => new int[4] { this.cube[1][0][1].B, this.cube[1][2][1].F, this.cube[1][1][0].L, this.cube[1][1][2].R };

        int[] findCenter(int color)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if ((i == 1 && (j == 1 || k == 1)) || (j == 1 && k == 1))
                        {
                            List<int> clist = this.cube[i][j][k].Colors();
                            if (clist.Contains(color)) return new int[] { i, j, k };
                        }
                    }
                }
            }
            return new int[] { -1, -1, -1 };
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

        bool IsAdjacent(int[] pos1, int[] pos2) => ((pos1[0] == pos2[0] + 1 || pos1[0] == pos2[0] - 1) && pos1[1] == pos2[1] && pos1[2] == pos2[2]) ||
                ((pos1[1] == pos2[1] + 1 || pos1[1] == pos2[1] - 1) && pos1[0] == pos2[0] && pos1[2] == pos2[2]) ||
                ((pos1[2] == pos2[2] + 1 || pos1[2] == pos2[2] - 1) && pos1[0] == pos2[0] && pos1[1] == pos2[1]);

        bool IsCrossEdgeRightPlaced(int[] pos)
        {
            foreach(int color in this.cube[pos[0]][pos[1]][pos[2]].Colors())
            {
                if (!IsAdjacent(pos, findCenter(color))) return false;
            }
            return true;
        }

        bool IsCrossEdgeRightOriented(int[] pos)
        {
            if (!IsCrossEdgeRightPlaced(pos)) return false;
            return this.cube[pos[0]][pos[1]][pos[2]].D == cross_color;
        }

        void FlipCrossEdge() // Must be Front Center Down
        {
            this.Rotate("F");
            this.Rotate("D'");
            this.Rotate("L");
            this.Rotate("D");
        }

        public void MakeCross()
        {
            if(IsCrossDone()) return;

            foreach(int color in SideColors())
            {
                int[] edge_pos = findEdge(cross_color, color);
                if(IsCrossEdgeRightOriented(edge_pos)) continue; 
                while (edge_pos[1] != 2)
                {
                    Rotate("y");
                    edge_pos = findEdge(cross_color, color);
                }
                if (IsCrossEdgeRightPlaced(edge_pos))
                {
                    FlipCrossEdge();
                    continue;
                }
                while (edge_pos[0] != 0)
                {
                    if (edge_pos[2] == 0)
                    {
                        Rotate("F");
                        Rotate("U");
                        Rotate("F'");
                    }
                    else if (edge_pos[2] == 2)
                    {
                        Rotate("F'");
                        Rotate("U'");
                        Rotate("F");
                    }
                    else
                    {
                        Rotate("F2");
                    }
                    edge_pos = findEdge(cross_color, color);
                }
                int edge_color = this.cube[edge_pos[0]][edge_pos[1]][edge_pos[2]].Colors().Sum() - cross_color;
                while(!IsAdjacent(edge_pos, findCenter(edge_color)))
                {
                    Rotate("U");
                    edge_pos = findEdge(cross_color, color);
                }
                while(findCenter(edge_color)[1] != 2)
                {
                    Rotate("y");
                }
                Rotate("F2");
                edge_pos = findEdge(cross_color, color);
                if (!IsCrossEdgeRightOriented(edge_pos))
                {
                    FlipCrossEdge();
                }
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
