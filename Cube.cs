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
            TopClockwise(0);
        }
        private void Uprime() {
            TopCounterClockwise(0);
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
            TopCounterClockwise(2);
        }
        private void Dprime() {
            TopClockwise(2);
        }
        private void D2() {
            this.D();
            this.D();
        }
        private void d() {
            this.D();
            this.E();
        }
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
        private void xprime() { }
        private void y() { }
        private void yprime() { }
        private void z() { }
        private void zprime() { }
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
