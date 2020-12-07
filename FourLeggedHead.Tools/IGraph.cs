using System;
using System.Collections.Generic;
using System.Text;

namespace FourLeggedHead.Tools
{
    public interface IGraph<T> :IEnumerable<IVertex> where T: struct
    {
        T? DistanceBetweenNeighbors(IVertex firstVertex, IVertex secondVertex);
        T DistanceBetweenVertices(IVertex firstVertex, IVertex secondVertex);
        T MaxDistance();
        T AddDistance(T firstDistance, T secondDistance);
        IEnumerable<IVertex> GetNeighbors(IVertex vertex);
    }
}
