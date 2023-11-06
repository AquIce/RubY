using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAN
{
    /// <summary>
    /// Classe statique contenant les id des couleurs
    /// </summary>
    public static class Color
    {
        /// <summary>
        /// Aucune couleur
        /// </summary>
        public const int NONE = -1;
        /// <summary>
        /// Blanc
        /// </summary>
        public const int WHITE = 0;
        /// <summary>
        /// Vert
        /// </summary>
        public const int GREEN = 1;
        /// <summary>
        /// Orange
        /// </summary>
        public const int ORANGE = 2;
        /// <summary>
        /// Bleu
        /// </summary>
        public const int BLUE = 3;
        /// <summary>
        /// Rouge
        /// </summary>
        public const int RED = 4;
        /// <summary>
        /// Jaune
        /// </summary>
        public const int YELLOW = 5;
    }

    /// <summary>
    /// Classe statique contenant les algorithmes nécessaires à la résolution du Rubik's Cube
    /// </summary>
    public class Algorithms
    {
        /// <summary>
        /// Algorithmes pour les cas F2L
        /// </summary>
        public static readonly Dictionary<string, string> F2L = new Dictionary<string, string>
        {
            /*
                000 -> Corner pos
                u/d/f/b/l/r -> Corner orientation (white pos)
                u/d/f/b/l/r -> Corner orientation (front color pos)
                111 -> Edge pos
                u/d/f/b/l/r -> Edge orientation (front color pos)
            */
            // Each F2L that needs it got a rotation cancellation (y)
            {
                "022fu 012u", "U R U' R'"
            },
            {
                "022rf 021f", "y' U' R' U R y"
            },
            {
                "022fu 010l", "y' R' U' R y"
            },
            {
                "022rf 001u", "R U R'"
            },
            {
                "022fu 001b", "U' R U' R' U y' R' U' R y"
            },
            {
                "022rf 010u", "U' R U R' U R U R'"
            },
            {
                "022fu 012r", "U' R U2 R' U y' R' U' R y"
            },
            {
                "022rf 021u", "R' U2 R2 U R2 U R"
            },
            {
                "022fu 021f", "y' U R' U R U' R' U' R U y"
            },
            {
                "022rf 012u", "U' R U' R' U R U R'"
            },
            {
                "022fu 001u", "U' R U R' U2 R U' R'"
            },
            {
                "022rf 010l", "y' U R' U' R U2 R' U R y"
            },
            {
                "022fu 010u", "U' R U2 R' U2 R U' R'"
            },
            {
                "022rf 001b", "y' U R' U2 R U2 R' U R y"
            },
            {
                "022ur 001u", "U R U2 R' U R U' R'"
            },
            {
                "022ur 010l", "y' U' R' U2 R U' R' U R y"
            },
            {
                "022ur 010u", "U2 R U R' U R U' R'"
            },
            {
                "022ur 001b", "F' L' U2 L F"
            },
            {
                "022fu 021u", "y' R' U R U2 y R U R'"
            },
            {
                "022rf 012r", "R U' R' U2 y' R' U' R y"
            },
            {
                "022ur 012u", "R U2 R' U' R U R' "
            },
            {
                "022ur 021f", "y' R' U2 R U R' U' R y"
            },
            {
                "022ur 021u", "U R U' R' U' R U' R' U R U' R'"
            },
            {
                "022ur 012r", "y' U' R' U R U R' U R U' R' U R y"
            },
            {
                "222df 012u", "U' F' R U R' U' R' F R"
            },
            {
                "222df 021f", "U R U' R' U' F' U F"
            },
            {
                "222fr 012u", "R U' R' U R U' R'"
            },
            {
                "222rd 021f", "y' R' U R U' R' U R y"
            },
            {
                "222fr 021f", "y' R' U' R U R' U' R y"
            },
            {
                "222rd 012u", "R U R' U' R U R'"
            },
            {
                "022ur 122r", "R U' R' U y' R' U R y"
            },
            {
                "022ur 122f", "U R U' R' U R U' R' U R U' R'"
            },
            {
                "022fu 122f", "U' R U' R' U2 R U' R'"
            },
            {
                "022rf 122f", "U R U R' U2 R U R'"
            },
            {
                "022fu 122r", "U' R U R' U y' R' U' R y"
            },
            {
                "022rf 122r", "U F' U' F U' R U R'"
            },
            {
                "222df 122f", ""
            },
            {
                "222df 122r", "R U' R' d R' U2 R U2 R' U R"
            },
            {
                "222fr 122f", "R U' R' U' R U R' U2 R U' R'"
            },
            {
                "222rd 122f", "R U' R' U R U2 R' U R U' R'"
            },
            {
                "222fr 122r", "F' U F U2 R U R' U R U' R'"
            },
            {
                "222rd 122r", "R U R' U' R U' R' U2 y' R' U' R y"
            }
        };

        /// <summary>
        /// Algorithmes pour les cas OLL
        /// </summary>
        public static readonly Dictionary<string, string> OLL = new Dictionary<string, string>
        {
            {
                "uuu uuu uuu", "U'" // To cancel the U in recognition
            },
            {
                "luu uuu fur", "R U2 R' U' R U' R'"
            },
            {
                "bub uuu fuf", "R U2 R' U' R U R' U' R U' R'"
            },
            {
                "buu uuu fuu", "r U R' U' r r r F R F'"
            },
            {
                "uuu uuu fuf", "R2 D R' U2 R D' R' U2 R'"
            },
            {
                "bur uuu uuf", "R U R' U R U2 R'"
            },
            {
                "lub uuu luf", "R U2 R2 U' R2 U' R2 U2 R"
            },
            {
                "uur uuu fuu", "y F' r U R' U' r r r F R"
            },
            {
                "bbu uuu ffu", "R U R' U' R' F R F'"
            },
            {
                "lbu uuu lfu", "F R U R' U' F'"
            },
            {
                "bbr luu luu", "r r r U2 R U R' U r"
            },
            {
                "luu luu ffr", "r U2 R' U' R U' r r r"
            },
            {
                "lbr uuu ufu", "R U R2 U' R' F R U R U' F'"
            },
            {
                "uur lur uur", "R' U' R' F R F' U R"
            },
            {
                "ubr uur fuu", "R' U' R U' R' U R U l U' R' U x"
            },
            {
                "buu uur ufr", "R U R' U R U' R' U' R' F R F'"
            },
            {
                "uuu uur ufu", "r U R' U' M U R U' R'"
            },
            {
                "ubu uuu ufu", "R U R' U' M' U R U' r r r"
            },
            {
                "buu luu ffu", "R' U' F U R U' R' F' R"
            },
            {
                "bbu luu fuu", "R U B' U' R' U R B R'"
            },
            {
                "ubr uur uur", "y R' U' F' U F R"
            },
            {
                "lbu luu luu", "f R U R' U' f'"
            },
            {
                "lbb uuu lff", "f R U R' U' R U R' U' f'"
            },
            {
                "bur lur fur", "R' U' R U' R' U y' R' U R B"
            },
            {
                "lbr uuu lfr", "y R' F R U R U' R2 F' R2 U' R' U R U R'"
            },
            {
                "lur lur lur", "r r r U' r U' R' U R U' R' U R r r r U r"
            },
            {
                "lub uur ffu", "R U R' U' R' F R2 U R' U' F'"
            },
            {
                "bbu uur luf", "R U R' U R' F R F' R U2 R'"
            },
            {
                "ubr luu fuu", "R U2 R2 F R F' R U2 R'"
            },
            {
                "uur uur ffu", "F R U' R' U' R U R' F'"
            },
            {
                "bbr uuu uff", "r U' r r r U' r U r r r y' R' U R"
            },
            {
                "lbb uuu ffu", "R' F R U R' F' R F U' F'"
            },
            {
                "bbr uuu lfu", "r r r U' r R' U' R U r r r U r"
            },
            {
                "lbu uuu ffr", "r U r r r R U R' U' r U' r r r"
            },
            {
                "ubu uur lur", "y R U R' U' R U' R' F' U' F R U R'"
            },
            {
                "uub luu uff", "y' F U R U2 R' U' R U2 R' U' F'"
            },
            {
                "bub uur ufu", "R U R' U R U2 R' F R U R' U' F'"
            },
            {
                "ubu uur fuf", "R' U' R U' R' U2 R F R U R' U' F'"
            },
            {
                "bur luu ffr", "F' L' U' L U L' U' L U F"
            },
            {
                "lub uur lff", "F R U R' U' R U R' U' F'"
            },
            {
                "lub luu lff", "r U' r r U r r U r r U' r"
            },
            {
                "lbb luu luf", "r r r U r r U' r r U' r r U r r r"
            },
            {
                "lbr luu lur", "r r r U' R U' R' U R U' R' U2 r"
            },
            {
                "lur luu lfr", "r U R' U R U' R' U R U2 r r r"
            },
            {
                "bur uur uff", "r U R' U R U2 r r r"
            },
            {
                "ubb uur fur", "r r r U' R U' R' U2 r"
            },
            {
                "bbr luu uuf", "r r r R2 U R' U R U2 R' U M'"
            },
            {
                "uub luu ffr", "M' R' U' R U' R' U2 R U' M\r\n"
            },
            {
                "bbu uuu ufr", "L F' L' U' L U F U' L'"
            },
            {
                "ubb uuu lfu", "R' F R U R' U' F' U R"
            },
            {
                "lbr lur lfr", "R U2 R2 F R F' U2 R' F R F'"
            },
            {
                "lbb lur lff", "F R U R' U' F' f R U R' U' f'"
            },
            {
                "bbr lur lfu", "f R U R' U' f' U' F R U R' U' F'"
            },
            {
                "lbu lur ffr", "f R U R' U' f' U F R U R' U' F'"
            },
            {
                "ubb lur lfu", "R U R' U R' F R F' U2 R' F R F'"
            },
            {
                "ubu lur fff", "y R U2 R2 F R F' U2 M' U R U' r r r"
            },
            {
                "ubu lur lfr", "M U R U R' U' M' R' F R F'"
            },
            {
                "ubu lur ufu", "M U R U R' U' M2 U R U' r r r"
            }
        };

        /// <summary>
        /// Algorithmes pour les cas PLL
        /// </summary>
        public static readonly Dictionary<string, string> PLL = new Dictionary<string, string>
        {
            {
                "-", "U'" // To cancel recognition U
            },
            {
                "Ua", "R U' R U R U R U' R' U' R2"
            },
            {
                "Ub", "R2 U R U R' U' R' U' R' U R'"
            },
            {
                "Z", "M2 U M2 U M' U2 M2 U2 M' U2"
            },
            {
                "H", "M2 U M2 U2 M2 U M2"
            },
            {
                "Aa", "x R' U R' D2 R U' R' D2 R2 x'"
            },
            {
                "Ab", "x R2 D2 R U R' D2 R U' R x'"
            },
            {
                "E", "x' R U' R' D R U R' D' R U R' D R U' R' D' x"
            },
            {
                "Ra", "R U' R' U' R U R D R' U' R D' R' U2 R' U'"
            },
            {
                "Rb", "R2 F R U R U' R' F' R U2 R' U2 R"
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
                "Gb", "F' U' F R2 u R' U R U' R u' R2"
            },
            {
                "Gc", "R2 U' R U' R U R' U R2 D' U R U' R' D U'"
            },
            {
                "Gd", "D' R U R' U' D R2 U' R U' R' U R' U R2 U"
            }
        };
    }

    /// <summary>
    /// Classe représentant un mini cube (27 composent un Rubik's Cube)
    /// </summary>
    public class MiniCube : ICloneable
    {
        /// <summary>
        /// La face supérieure
        /// </summary>
        public int U;
        /// <summary>
        /// La face inférieure
        /// </summary>
        public int D;
        /// <summary>
        /// La face frontale
        /// </summary>
        public int F;
        /// <summary>
        /// La face arrière
        /// </summary>
        public int B;
        /// <summary>
        /// Face gauche
        /// </summary>
        public int L;
        /// <summary>
        /// Face droite
        /// </summary>
        public int R;

        /// <summary>
        /// Crée un minicube avec les couleurs spécifiées
        /// </summary>
        /// <param name="U">Couleur de la face supérieure</param>
        /// <param name="D">Couleur de la face inférieure</param>
        /// <param name="F">Couleur de la face frontale</param>
        /// <param name="B">Couleur de la face arrière</param>
        /// <param name="L">Couleur de la face gauche</param>
        /// <param name="R">Couleur de la face droite</param>
        public MiniCube(int U, int D, int F, int B, int L, int R)
        {
            this.U = U;
            this.D = D;
            this.F = F;
            this.B = B;
            this.L = L;
            this.R = R;
        }

        /// <summary>
        /// Clone le mini cube
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Effectue une rotation sur le mini cube
        /// </summary>
        /// <param name="way">Le type de rotation, en notation algorithmique</param>
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

        /// <summary>
        /// Retourne la liste des couleurs du mini cube
        /// </summary>
        /// <returns>Liste des couleurs</returns>
        public List<int> Colors()
        {
            List<int> ints = new List<int>();
            if (this.U != Color.NONE) ints.Add(this.U);
            if (this.D != Color.NONE) ints.Add(this.D);
            if (this.F != Color.NONE) ints.Add(this.F);
            if (this.B != Color.NONE) ints.Add(this.B);
            if (this.R != Color.NONE) ints.Add(this.R);
            if (this.L != Color.NONE) ints.Add(this.L);
            return ints;
        }

        /// <summary>
        /// Retourne la face du mini cube sur laquelle se trouve une certaine couleur
        /// </summary>
        /// <param name="color">La couleur à chercher</param>
        /// <returns>La face sur laquelle se trouve la couleur</returns>
        public char GetColorOritentation(int color)
        {
            if (this.U == color) return 'u';
            if (this.D == color) return 'd';
            if (this.F == color) return 'f';
            if (this.B == color) return 'b';
            if (this.R == color) return 'r';
            if (this.L == color) return 'l';
            return ' ';
        }
    }

    /// <summary>
    /// Classe représentant un Rubik's Cube
    /// </summary>
    public class Cube
    {
        /// <summary>
        /// Suite de rotations effectuées sur le Rubik's Cube pour le résoudre
        /// </summary>
        public string solution = "";

        /// <summary>
        /// Représentation interne du cube
        /// </summary>
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

        #region Solving

        private int cross_color = Color.NONE;

        private List<string> moves = new List<string>();

        #region Find Pieces
        private int[] findCenter(int color)
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

        private int[] findEdge(int color1, int color2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i == 1 || j == 1 || k == 1)
                        {
                            List<int> clist = this.cube[i][j][k].Colors();
                            if (clist.Contains(color1) && clist.Contains(color2)) return new int[] { i, j, k };
                        }
                    }
                }
            }
            return new int[] { -1, -1, -1 };
        }

        private int[] findCorner(int color1, int color2, int color3)
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
        #endregion

        #region Cross

        private bool IsCrossDone()
        {
            cross_color = this.cube[2][1][1].D;
            return (this.cube[2][0][1].D == cross_color) && (this.cube[2][0][1].B == this.cube[1][0][1].B) &&
                (this.cube[2][1][0].D == cross_color) && (this.cube[2][1][0].L == this.cube[1][1][0].L) &&
                (this.cube[2][2][1].D == cross_color) && (this.cube[2][2][1].F == this.cube[1][2][1].F) &&
                (this.cube[2][1][2].D == cross_color) && (this.cube[2][1][2].R == this.cube[1][1][2].R);
        }

        private int[] SideColors() => new int[4] { this.cube[1][0][1].B, this.cube[1][2][1].F, this.cube[1][1][0].L, this.cube[1][1][2].R };


        private bool IsAdjacent(int[] pos1, int[] pos2) => ((pos1[0] == pos2[0] + 1 || pos1[0] == pos2[0] - 1) && pos1[1] == pos2[1] && pos1[2] == pos2[2]) ||
                ((pos1[1] == pos2[1] + 1 || pos1[1] == pos2[1] - 1) && pos1[0] == pos2[0] && pos1[2] == pos2[2]) ||
                ((pos1[2] == pos2[2] + 1 || pos1[2] == pos2[2] - 1) && pos1[0] == pos2[0] && pos1[1] == pos2[1]);

        private bool IsCrossEdgeRightPlaced(int[] pos)
        {
            foreach (int color in this.cube[pos[0]][pos[1]][pos[2]].Colors())
            {
                if (!IsAdjacent(pos, findCenter(color))) return false;
            }
            return true;
        }

        private bool IsCrossEdgeRightOriented(int[] pos)
        {
            if (!IsCrossEdgeRightPlaced(pos)) return false;
            return this.cube[pos[0]][pos[1]][pos[2]].D == cross_color;
        }

        private void FlipCrossEdge() // Must be Front Center Down
        {
            this.Rotate("F");
            this.Rotate("D'");
            this.Rotate("L");
            this.Rotate("D");
        }

        private void SolveCross()
        {
            if (IsCrossDone()) return;

            foreach (int color in SideColors())
            {
                int[] edge_pos = findEdge(cross_color, color);
                if (IsCrossEdgeRightOriented(edge_pos)) continue;
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
                while (!IsAdjacent(edge_pos, findCenter(edge_color)))
                {
                    Rotate("U");
                    edge_pos = findEdge(cross_color, color);
                }
                while (findCenter(edge_color)[1] != 2)
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

        #endregion

        #region F2L

        private bool PrepareSlot(int[] pos)
        {
            if (pos[0] == 0) { return true; }
            if (pos[1] == 0 && pos[2] == 0)
            {
                Rotate("U'"); // For the edge not to go in the slot
                Rotate("L");
                Rotate("U");
                Rotate("L'");
                Rotate("U'");
                return true;
            }
            if (pos[1] == 0 && pos[2] == 2)
            {
                Rotate("U"); // For the edge not to go in the slot
                Rotate("R'");
                Rotate("U'");
                Rotate("R");
                Rotate("U");
                return true;
            }
            if (pos[1] == 2 && pos[2] == 0)
            {
                Rotate("L'");
                Rotate("U'");
                Rotate("L");
                Rotate("U");
                return true;
            }
            return false;
        }

        private void Slot()
        {
            int[] edge_pos = findEdge(this.cube[1][1][2].R, this.cube[1][2][1].F);

            bool alignLL = PrepareSlot(edge_pos);

            while (alignLL && (edge_pos[1] != 2 || edge_pos[2] != 1))
            {
                Rotate("U");
                edge_pos = findEdge(this.cube[1][1][2].R, this.cube[1][2][1].F);
            }

            int[] corner_pos = findCorner(this.cube[1][1][2].R, this.cube[1][2][1].F, this.cross_color);
            PrepareSlot(corner_pos);
        }

        private void SolveF2L()
        {
            for (int i = 0; i < 4; i++)
            {
                this.solution += $"Slot {i + 1}: ";
                Slot();

                int center_front = this.cube[1][2][1].F;
                int[] corner_pos = findCorner(this.cross_color, center_front, this.cube[1][1][2].R);
                MiniCube corner = this.cube[corner_pos[0]][corner_pos[1]][corner_pos[2]];
                int[] edge_pos = findEdge(center_front, this.cube[1][1][2].R);
                MiniCube edge = this.cube[edge_pos[0]][edge_pos[1]][edge_pos[2]];

                string pos_str = $"{corner_pos[0]}{corner_pos[1]}{corner_pos[2]}" +
                    $"{corner.GetColorOritentation(this.cross_color)}{corner.GetColorOritentation(center_front)} " +
                    $"{edge_pos[0]}{edge_pos[1]}{edge_pos[2]}" +
                    $"{edge.GetColorOritentation(center_front)}";

                while (!Algorithms.F2L.ContainsKey(pos_str))
                {
                    Rotate("U");
                    corner_pos = findCorner(this.cross_color, center_front, this.cube[1][1][2].R);
                    corner = this.cube[corner_pos[0]][corner_pos[1]][corner_pos[2]];
                    edge_pos = findEdge(center_front, this.cube[1][1][2].R);
                    edge = this.cube[edge_pos[0]][edge_pos[1]][edge_pos[2]];
                    pos_str = $"{corner_pos[0]}{corner_pos[1]}{corner_pos[2]}" +
                        $"{corner.GetColorOritentation(this.cross_color)}{corner.GetColorOritentation(center_front)} " +
                        $"{edge_pos[0]}{edge_pos[1]}{edge_pos[2]}" +
                        $"{edge.GetColorOritentation(center_front)}";
                }

                string alg = Algorithms.F2L[pos_str];

                if (alg != "")
                {
                    foreach (string move in alg.Split(' '))
                    {
                        Rotate(move);
                    }
                }

                Rotate("y");
            }
        }

        #endregion

        #region OLL

        void SolveOLL()
        {
            string oll_case;

            do
            {
                Rotate("U");
                oll_case = "";
                foreach (MiniCube[] line in this.cube[0])
                {
                    foreach (MiniCube cube in line)
                    {
                        oll_case += cube.GetColorOritentation(this.cube[0][1][1].U);
                    }
                    oll_case += " ";
                }
                oll_case = oll_case.Trim();
            } while (!Algorithms.OLL.ContainsKey(oll_case));

            foreach (string move in Algorithms.OLL[oll_case].Split(' '))
            {
                Rotate(move);
            }
        }

        #endregion

        #region PLL

        private bool IsTopCrossDone()
        {
            bool done = false;
            for(int i = 0; i < 4; i++)
            {
                if (
                    this.cube[0][2][1].F == this.cube[1][2][1].F &&
                    this.cube[0][1][2].R == this.cube[1][1][2].R &&
                    this.cube[0][0][1].B == this.cube[1][0][1].B &&
                    this.cube[0][1][0].L == this.cube[1][1][0].L
                ) {
                    done = true;
                    Rotate("U");
                }
            }
            return done;
        }

        /// <summary>
        /// Recognizes the PLL case
        /// </summary>
        /// <returns>The name of the PLL case</returns>
        private string PLLRecognitionKey()
        {
            // Solved
            if(
                (this.cube[0][0][0].B == this.cube[0][0][1].B && this.cube[0][0][1].B == this.cube[0][0][2].B) && // Back bar
                (this.cube[0][2][0].F == this.cube[0][2][1].F && this.cube[0][2][1].F == this.cube[0][2][2].F) // Front bar
            ) { return "-"; }
            // EPLL
            if(
                this.cube[0][0][0].B == this.cube[0][0][2].B &&// Back corners
                this.cube[0][2][0].F == this.cube[0][2][2].F // Front corners
            ) {
                if (this.cube[0][2][1].F == this.cube[0][2][2].F)
                { // Bar in the front
                    if (this.cube[0][1][2].R == this.cube[0][0][2].B) // Middle of the right headlights from the back
                    { return "Ua"; }
                    else if (this.cube[0][1][2].R == this.cube[0][0][0].L) // Middle of the right headlights from the left
                    { return "Ub"; }
                }
                else if (
                    this.cube[0][2][1].F == this.cube[0][0][0].B && // Front - back
                    this.cube[0][1][2].R == this.cube[0][0][0].L // Right left
                ) { return "H"; }
                else if (
                    this.cube[0][2][1].F == this.cube[0][2][2].R && // Front - right
                    this.cube[0][0][1].B == this.cube[0][0][0].L // Back - left
                ) { return "Z"; }
            }
            // CPLL
            if(IsTopCrossDone()) {
                if (this.cube[0][2][0].F == this.cube[0][2][1].F) { // Front left block
                    if (this.cube[0][0][0].B == this.cube[0][0][2].B) // Headlights on the back
                    { return "Aa"; }
                    else if (this.cube[0][0][2].R == this.cube[0][2][2].R) // Headlights on the right
                    { return "Ab";  }
                }
                else if (this.cube[0][2][2].F == this.cube[0][1][2].R) { return "E"; }   
            }
            // Adjacent swap
            if(
                this.cube[0][0][0].B == this.cube[0][0][1].B &&
                this.cube[0][2][0].B == this.cube[0][2][1].B
            ) { return "T"; }
            if(
                this.cube[0][0][0].L == this.cube[0][1][0].L &&
                this.cube[0][0][0].L == this.cube[0][2][0].L
            ) // Left bar
            {
                if (this.cube[0][2][0].F == this.cube[0][2][1].F) {
                    Rotate("U"); // To setup for alg execution
                    return "Ja";
                }
                if (this.cube[0][0][0].B == this.cube[0][0][1].B) { return "Jb"; }
                else { return "F";  }
            }
            if(this.cube[0][0][0].L == this.cube[0][2][0].L) { // Headlights on the left
                if(this.cube[0][2][0].F == this.cube[0][2][1].F) { return "Ra"; }
                if(this.cube[0][0][0].B == this.cube[0][0][1].B) { return "Rb"; }
                if (this.cube[0][2][1].F == this.cube[0][2][2].F) { return "Ga"; }
                if (this.cube[0][0][2].L == this.cube[0][1][2].L) { return "Gb"; }
                if (this.cube[0][0][1].B == this.cube[0][0][2].B) {
                    Rotate("R2"); // To setup for alg execution
                    return "Gc";
                }
                if (this.cube[0][1][2].R == this.cube[0][2][2].L) { return "Gd"; }
            }
            return "NO_MATCH";
        }

        private void AUF()
        {
            while (this.cube[0][2][1].F != this.cube[1][2][1].F) { Rotate("U"); }
        }

        private void SolvePLL()
        {
            string pll_case;

            do
            {
                Rotate("U");
                pll_case = PLLRecognitionKey();
            } while (!Algorithms.PLL.ContainsKey(pll_case));

            foreach (string move in Algorithms.OLL[pll_case].Split(' '))
            {
                Rotate(move);
            }
            AUF();
        }

        #endregion

        /// <summary>
        /// Résoudre le Rubik's Cube
        /// </summary>
        public void Solve()
        {
            this.moves = new List<string>();
            this.solution = "Cross: ";
            SolveCross();
            this.solution += "\nF2L: ";
            SolveF2L();
            this.solution += "\nOLL: ";
            SolveOLL();
            this.solution += "\nPLL: ";
            //SolvePLL();
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

        private void U()
        {
            this.TopClockwise(0);
        }
        private void Uprime()
        {
            this.TopCounterClockwise(0);
        }
        private void U2()
        {
            this.U();
            this.U();
        }
        private void u()
        {
            this.U();
            this.Eprime();
        }
        private void D()
        {
            this.TopCounterClockwise(2);
        }
        private void Dprime()
        {
            this.TopClockwise(2);
        }
        private void D2()
        {
            this.D();
            this.D();
        }
        private void d()
        {
            this.D();
            this.E();
        }
        #endregion

        #region F_B_Layers_Move
        private void F()
        {
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
        private void Fprime()
        {
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
        private void F2()
        {
            this.F();
            this.F();
        }
        private void f()
        {
            this.F();
            this.S();
        }
        private void B()
        {
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
        private void Bprime()
        {
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
        private void B2()
        {
            this.B();
            this.B();
        }
        private void b()
        {
            this.B();
            this.Sprime();
        }
        #endregion

        #region R_L_Layers_Move
        private void R()
        {
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
        private void Rprime()
        {
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
        private void R2()
        {
            this.R();
            this.R();
        }
        private void r()
        {
            this.R();
            this.Mprime();
        }
        private void L()
        {
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
        private void Lprime()
        {
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
        private void L2()
        {
            this.L();
            this.L();
        }
        private void l()
        {
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
        private void x()
        {
            this.R();
            this.Mprime();
            this.Lprime();
        }
        private void xprime()
        {
            this.Rprime();
            this.M();
            this.L();
        }
        private void x2()
        {
            this.x();
            this.x();
        }
        private void y()
        {
            this.U();
            this.Eprime();
            this.Dprime();
        }
        private void yprime()
        {
            this.Uprime();
            this.E();
            this.D();
        }
        private void y2()
        {
            this.y();
            this.y();
        }
        private void z()
        {
            this.F();
            this.S();
            this.Bprime();
        }
        private void zprime()
        {
            this.Fprime();
            this.Sprime();
            this.B();
        }
        private void z2()
        {
            this.z();
            this.z();
        }
        #endregion

        /// <summary>
        /// Fait tourner un layer du cube
        /// </summary>
        /// <param name="way">Le type de rotation, en notation algorithmique</param>
        public void Rotate(string way)
        {
            this.solution += way + " ";
            this.moves.Add(way);
            switch (way)
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

        /// <summary>
        /// Représentation numérique de la face demandée
        /// </summary>
        /// <param name="face">Face concernée</param>
        /// <returns>La représentation numérique</returns>
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
                    for (int i = 0; i < this.cube[0].Length; i++)
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
                        for (int j = 0; j < mc[i].Length; j++)
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
    }
}
