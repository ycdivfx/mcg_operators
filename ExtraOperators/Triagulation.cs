using System.Collections.Generic;
using Autodesk.Sequences;
using Microsoft.Xna.Framework;

namespace MCG.ExtraOperators
{
    public static class Triagulation
    {
        /// <summary>
        ///     Calculates the area of the contour defined by the given points.
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
        internal static float Area(IArray<Vector3> contour)
        {
            var n = contour.Count;
            var area = 0f;
            for (int p = n - 1, q = 0; q < n; p = q++)
                area += contour[p].X * contour[q].Y - contour[q].X * contour[p].Y;
            return area * .5f;
        }

        /// <summary>
        ///     Verivies if its inside of a triangle
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <param name="Px"></param>
        /// <param name="Py"></param>
        /// <returns></returns>
        internal static bool InsideTriangle(float Ax, float Ay, float Bx, float By, float Cx, float Cy, float Px,
            float Py)
        {
            var ax = Cx - Bx;
            var ay = Cy - By;
            var bx = Ax - Cx;
            var by = Ay - Cy;
            var cx = Bx - Ax;
            var cy = By - Ay;
            var apx = Px - Ax;
            var apy = Py - Ay;
            var bpx = Px - Bx;
            var bpy = Py - By;
            var cpx = Px - Cx;
            var cpy = Py - Cy;

            var a_CROSS_bp = ax * bpy - ay * bpx;
            var c_CROSS_ap = cx * apy - cy * apx;
            var b_CROSS_cp = bx * cpy - by * cpx;

            return a_CROSS_bp >= 0.0f && b_CROSS_cp >= 0.0f && c_CROSS_ap >= 0.0f;
        }

        /// <summary>
        /// Contour cap meshing.
        /// </summary>
        /// <param name="contour"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="n"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        internal static bool Snip(IArray<Vector3> contour, int u, int v, int w, int n, IList<int> indices)
        {
            int p;

            var ax = contour[indices[u]].X;
            var ay = contour[indices[u]].Y;

            var bx = contour[indices[v]].X;
            var by = contour[indices[v]].Y;

            var cx = contour[indices[w]].X;
            var cy = contour[indices[w]].Y;

            if (float.Epsilon > (bx - ax) * (cy - ay) - (by - ay) * (cx - ax)) return false;

            for (p = 0; p < n; p++)
            {
                if (p == u || p == v || p == w) continue;
                var px = contour[indices[p]].X;
                var py = contour[indices[p]].Y;
                if (InsideTriangle(ax, ay, bx, by, cx, cy, px, py)) return false;
            }

            return true;
        }
    }
}