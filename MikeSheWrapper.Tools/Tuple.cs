using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public struct Tuple<T1, T2>
  {
    private readonly T1 first;
    public T1 First
    {
      get { return first; }
    }

    private readonly T2 second;
    public T2 Second
    {
      get { return second; }
    }

    public Tuple(T1 f, T2 s)
    {
      first = f;
      second = s;
    }

  }
}
