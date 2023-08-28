using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public static char Str(int color) => new char[] { 'W', 'G', 'O', 'B', 'R', 'Y' }[color];
    }

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
    }

    public class Cube
    {
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

        private void U() {
            MiniCube[][] layer = this.cube[0].Clone() as MiniCube[][];
            for(int i = 0; i < layer.Length; i++)
            {
                for(int j = 0; j < layer[i].Length; j++)
                {
                    this.cube[0][j][2 - i] = layer[i][j];
                }
            }
            for(int i = 0; i < cube[0].Length; i++)
            {
                for(int j = 0; j <= cube[0][i].Length;j++)
                {
                    this.cube[0][i][j] = 
                }
            }
        }
        private void Uprime() { }
        private void U2() { }
        private void u() { }
        private void D() { }
        private void Dprime() { }
        private void D2() { }
        private void d() { }
        private void F() { }
        private void Fprime() { }
        private void F2() { }
        private void f() { }
        private void B() { }
        private void Bprime() { }
        private void B2() { }
        private void b() { }
        private void R() { }
        private void Rprime() { }
        private void R2() { }
        private void r() { }
        private void L() { }
        private void Lprime() { }
        private void L2() { }
        private void l() { }
        private void x() { }
        private void y() { }
        private void z() { }
        private void M() { }
        private void Mprime() { }
        private void M2() { }
        private void E() { }
        private void Eprime() { }
        private void E2() { }
        private void S() { }
        private void Sprime() { }
        private void S2() { }

        public void Rotate(string way)
        {
            switch(way)
            {
                case "U":
                    U();
                    break;
                case "U'":
                    break;
                case "U2":
                    break;
                case "u":
                    break;
                case "D":
                    break;
                case "D'":
                    break;
                case "D2":
                    break;
                case "d":
                    break;
                case "F":
                    break;
                case "F'":
                    break;
                case "F2":
                    break;
                case "f":
                    break;
                case "B":
                    break;
                case "B'":
                    break;
                case "B2":
                    break;
                case "b":
                    break;
                case "R":
                    break;
                case "R'":
                    break;
                case "R2":
                    break;
                case "r":
                    break;
                case "L":
                    break;
                case "L'":
                    break;
                case "L2":
                    break;
                case "l":
                    break;
                case "x":
                    break;
                case "y":
                    break;
                case "z":
                    break;
                case "M":
                    break;
                case "M'":
                    break;
                case "M2":
                    break;
                case "E":
                    break;
                case "E'":
                    break;
                case "E2":
                    break;
                case "S":
                    break;
                case "S'":
                    break;
                case "S2":
                    break;
            }
        }

        public string[] Face(char face)
        {
            string[] str = new string[3];
            MiniCube[][] mc;
            switch (face)
            {
                case 'U':
                    for(int i = 0; i < this.cube[0].Length; i++)
                    {
                        foreach(var mcube in this.cube[0][i])
                        {
                            str[i] += Color.Str(mcube.U);
                        }
                    }
                    return str;
                case 'D':
                    for (int i = 0; i < this.cube[2].Length; i++)
                    {
                        foreach (var mcube in this.cube[2][i])
                        {
                            str[i] += Color.Str(mcube.D);
                        }
                    }
                    return str;
                case 'F':
                    mc = new MiniCube[][] {
                        this.cube[0][2],
                        this.cube[1][2],
                        this.cube[2][2],
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        foreach (var mcube in mc[i])
                        {
                            str[i] += Color.Str(mcube.F);
                        }
                    }
                    return str;
                case 'B':
                    mc = new MiniCube[][] {
                        this.cube[0][0],
                        this.cube[1][0],
                        this.cube[2][0],
                    };
                    for (int i = 0; i < mc.Length; i++)
                    {
                        foreach (var mcube in mc[i])
                        {
                            str[i] += Color.Str(mcube.B);
                        }
                    }
                    return str;
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
                        foreach (var mcube in mc[i])
                        {
                            str[i] += Color.Str(mcube.L);
                        }
                    }
                    return str;
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
                        foreach (var mcube in mc[i])
                        {
                            str[i] += Color.Str(mcube.R);
                        }
                    }
                    return str;
                default:
                    return str;
            }
        }
    }
}
