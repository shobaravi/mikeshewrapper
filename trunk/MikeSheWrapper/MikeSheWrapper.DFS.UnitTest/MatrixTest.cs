using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;


namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class MatrixTest
  {
    [Test]
    public void ConstuctorTest()
    {
      Matrix M = new Matrix(10, 30);

      if (M[5, 5] == 0)
        M[5, 5] = 55;
    }

    [Test]
    public void ConstructFromFloatTest()
    {

      float[] _arr = new float[200];

      for (int i = 0; i < 200; i++)
        _arr[i] = i;

    }
  }
}
