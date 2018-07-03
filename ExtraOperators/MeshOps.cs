// Based on http://www.flipcode.com/archives/Efficient_Polygon_Triangulation.shtml
// Version 1.0
// Date: 02 Set 2015
// History
//  - First implementation - Daniel Santana
//

using System.Collections.Generic;
using System.Linq;
using Autodesk.Geometry3D;
using Autodesk.Sequences;
using Microsoft.Xna.Framework;
using ViperEngine;

namespace MCG.ExtraOperators
{
    [Category("Extra.Geometry")]
    public static class MeshOps
    {
        [Category("Mesh")]
        [Description("This is a simple contour cap. Doesn't support holes.")]
        public static TriMesh Mesh_From_Vertices(IArray<Vector3> contour)
        {
            if (contour.Count < 3) return TriMesh.EmptyMesh;

            var n = contour.Count;
            var result = new List<int>(n*3);
            var indices = Enumerable.Range(0, n).ToList();
            if (0.0f >= Triagulation.Area(contour))
                indices = indices.Select(i => (n - 1) - i).ToList();

            var nv = n;

            /*  remove nv-2 Vertices, creating 1 triangle every time */
            var count = 2*nv; /* error detection */

            for (int m = 0, v = nv - 1; nv > 2;)
            {
                /* if we loop, it is probably a non-simple polygon */
                if (0 >= (count--))
                {
                    //** Triangulate: ERROR - probable bad polygon!
                    return TriMesh.EmptyMesh;
                }

                /* three consecutive vertices in current polygon, <u,v,w> */
                var u = v;
                if (nv <= u) u = 0; /* previous */
                v = u + 1;
                if (nv <= v) v = 0; /* new v    */
                var w = v + 1;
                if (nv <= w) w = 0; /* next     */

                if (Triagulation.Snip(contour, u, v, w, nv, indices))
                {
                    int s, t;

                    /* true names of the vertices */
                    var a = indices[u];
                    var b = indices[v];
                    var c = indices[w];

                    /* output Triangle */
                    result.Add(a);
                    result.Add(b);
                    result.Add(c);

                    m++;

                    /* remove v from remaining polygon */
                    for (s = v, t = v + 1; t < nv; s++, t++) indices[s] = indices[t];
                    nv--;

                    /* resest error detection counter */
                    count = 2*nv;
                }
            }
            return new TriMesh(contour, result.ToIArray());
        }
    }
}