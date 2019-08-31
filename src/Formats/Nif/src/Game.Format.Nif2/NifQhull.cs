/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

namespace Niflib
{
    public static class NifQHull
    {
        public static Triangle[] compute_convex_hull(Vector3[] verts)
        {
            return null;
         //   vector<Triangle> tris;
         //   int dim = 3;                  /* dimension of points */
         //   int numpoints = 0;          /* number of points */
         //   coordT* points = 0;         /* array of coordinates for each point */
         //   boolT ismalloc = 0;         /* True if qhull should free points in qh_freeqhull() or reallocation */
         //   char flags[] = "qhull i Qt"; /* option flags for qhull, see qh_opt.htm */
         //   FILE* outfile = stdout;    /* output from qh_produce_output()			
							  //use NULL to skip qh_produce_output() */
         //   FILE* errfile = stderr;    /* error messages from qhull code */
         //   int exitcode;             /* 0 if no error from qhull */
         //   facetT* facet;            /* set by FORALLfacets */
         //   int curlong, totlong;     /* memory remaining after qh_memfreeshort */

         //   vertexT* vertex, **vertexp;
         //   setT* vertices;

         //   numpoints = verts.size();
         //   points = new coordT[3 * verts.size()];
         //   for (int i = 0, n = verts.size(); i < n; ++i)
         //   {
         //       points[i * 3 + 0] = verts[i].x;
         //       points[i * 3 + 1] = verts[i].y;
         //       points[i * 3 + 2] = verts[i].z;
         //   }

         //   /* initialize dim, numpoints, points[], ismalloc here */
         //   exitcode = qh_new_qhull(dim, numpoints, points, ismalloc, flags, outfile, errfile);
         //   if (!exitcode)
         //   { /* if no error */
         //     /* 'qh facet_list' contains the convex hull */
         //       FORALLfacets {
         //           vertices = qh_facet3vertex(facet);
         //           if (qh_setsize(vertices) == 3)
         //           {
         //               Triangle tri;
         //               int i = 0;
         //               FOREACHvertex_(vertices) {
         //                   tri[i++] = qh_pointid(vertex->point);
         //               }
         //               tris.push_back(tri);
         //           }
         //           qh_settempfree(&vertices);
         //       }
         //   }
         //   qh_freeqhull(!qh_ALL);
         //   qh_memfreeshort(&curlong, &totlong);
         //   if (curlong || totlong)
         //       fprintf(errfile, "qhull internal warning (main): did not free %d bytes of long memory (%d pieces)\n",
         //       totlong, curlong);
         //   delete[] points;
         //   return tris;
        }
    }
}