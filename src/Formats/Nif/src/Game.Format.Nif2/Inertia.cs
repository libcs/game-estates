using System;
using System.Collections.Generic;

namespace Niflib
{
    public class Inertia
    {
        /*! Return mass and inertia matrix for a sphere of given radius and
        *	density.
        */
        delegate void fnCalcMassPropertiesSphere(float radius, float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        /*! Return mass and inertia matrix for a box of given size and
        *   density.
        */
        delegate void fnCalcMassPropertiesBox(Vector3 size, float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        /*! Return mass and inertia matrix for a cylinder of given radius, 
        *   height and density.
        */
        delegate void fnCalcMassPropertiesCylinder(Vector3 startAxis, Vector3 endAxis, float radius, float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        /*! Return mass and inertia matrix for a capsule of given radius, 
        *	height and density.
        */
        delegate void fnCalcMassPropertiesCapsule(Vector3 startAxis, Vector3 endAxis, float radius, float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        /*! Return mass and inertia matrix for a capsule of given radius, 
        *	height and density.
        */
        delegate void fnCalcMassPropertiesPolyhedron(int nVerts, Vector3 verts, int nTris, Triangle tris, float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        /*! Combine mass properties for a number of objects */
        delegate void fnCombineMassProperties(int nItems, float masses, float volumes, Vector3 centers, InertiaMatrix inertias, Matrix44 transforms, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia);

        // Define external strategy helpers
        static fnCalcMassPropertiesSphere extCalcMassPropertiesSphereRoutine = null;
        static fnCalcMassPropertiesBox extCalcMassPropertiesBoxRoutine = null;
        static fnCalcMassPropertiesCylinder extCalcMassPropertiesCylinderRoutine = null;
        static fnCalcMassPropertiesCapsule extCalcMassPropertiesCapsuleRoutine = null;
        static fnCalcMassPropertiesPolyhedron extCalcMassPropertiesPolyhedronRoutine = null;
        static fnCombineMassProperties extCombineMassPropertiesRoutine = null;

        /*! Define external MassProperty calculation method for Sphere */
        static void SetCalcMassPropertiesSphere(fnCalcMassPropertiesSphere extRoutine) => extCalcMassPropertiesSphereRoutine = extRoutine;
        /*! Define external MassProperty calculation method for Box */
        static void SetCalcMassPropertiesBox(fnCalcMassPropertiesBox extRoutine) => extCalcMassPropertiesBoxRoutine = extRoutine;
        /*! Define external MassProperty calculation method for Cylinder */
        static void SetCalcMassPropertiesCylinder(fnCalcMassPropertiesCylinder extRoutine) => extCalcMassPropertiesCylinderRoutine = extRoutine;
        /*! Define external MassProperty calculation method for Capsule */
        static void SetCalcMassPropertiesCapsule(fnCalcMassPropertiesCapsule extRoutine) => extCalcMassPropertiesCapsuleRoutine = extRoutine;
        /*! Define external MassProperty calculation method for Polyhedron */
        static void SetCalcMassPropertiesPolyhedron(fnCalcMassPropertiesPolyhedron extRoutine) => extCalcMassPropertiesPolyhedronRoutine = extRoutine;
        /*! Define external mass property combination routine */
        static void SetCombineMassProperties(fnCombineMassProperties extRoutine) => extCombineMassPropertiesRoutine = extRoutine;

        /*! Return mass and inertia matrix for a sphere of given radius and
        *	density.
        */
        public static void CalcMassPropertiesSphere(float radius,
            float density, bool solid,
            out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            if (extCalcMassPropertiesSphereRoutine != null)
            {
                extCalcMassPropertiesSphereRoutine(radius,
                    density, solid,
                    out mass, out volume, out center, out inertia);
                return;
            }
            float inertiaScalar;
            if (solid)
            {
                mass = density * (4.0f * (float)Math.PI * (float)Math.Pow(radius, 3.0f)) / 3.0f;
                inertiaScalar = (2.0f * mass * (float)Math.Pow(radius, 2.0f)) / 5.0f;
            }
            else
            {
                mass = density * 4.0f * (float)Math.PI * (float)Math.Pow(radius, 2.0f);
                inertiaScalar = (2.0f * mass * (float)Math.Pow(radius, 2.0f)) / 3.0f;
            }
            center = new Vector3();
            inertia = InertiaMatrix.IDENTITY;
            inertia[0][0] = inertia[1][1] = inertia[2][2] = inertiaScalar;
        }

        /*! Return mass and inertia matrix for a box of given size and
         *   density.
         */
        static void CalcMassPropertiesBox(Vector3 size,
            float density, bool solid,
            out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            if (extCalcMassPropertiesBoxRoutine != null)
            {
                extCalcMassPropertiesBoxRoutine(size,
                    density, solid,
                    out mass, out volume, out center, out inertia);
                return;
            }
            Vector3 tmp;
            if (solid)
            {
                mass = density * (size[0] * size[1] * size[2]);
                tmp[0] = mass * (float)Math.Pow(size[0], 2.0f) / 12.0f;
                tmp[1] = mass * (float)Math.Pow(size[1], 2.0f) / 12.0f;
                tmp[2] = mass * (float)Math.Pow(size[2], 2.0f) / 12.0f;
            }
            else
            {
                mass = density * ((size[0] * size[1] * 2.0f) + (size[0] * size[2] * 2.0f) + (size[1] * size[2] * 2.0f));
                tmp[0] = mass * (float)Math.Pow(size[0], 2.0f) / 6.0f;
                tmp[1] = mass * (float)Math.Pow(size[1], 2.0f) / 6.0f;
                tmp[2] = mass * (float)Math.Pow(size[2], 2.0f) / 6.0f;
                //TODO: just guessing here, calculate it
            }
            inertia = InertiaMatrix.IDENTITY;
            inertia[0][0] = tmp[1] + tmp[2];
            inertia[1][1] = tmp[2] + tmp[0];
            inertia[2][2] = tmp[0] + tmp[1];
            center = new Vector3();
        }

        /*! Return mass and inertia matrix for a cylinder of given radius, 
         *   height and density.
         */
        static void CalcMassPropertiesCylinder(Vector3 startAxis, Vector3 endAxis, float radius,
            float density, bool solid,
            out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            if (extCalcMassPropertiesCylinderRoutine != null)
            {
                extCalcMassPropertiesCylinderRoutine(startAxis, endAxis, radius,
                    density, solid,
                    out mass, out volume, out center, out inertia);
                return;
            }
            var height = (startAxis - endAxis).Magnitude();
            center = (startAxis + endAxis) / 2.0f;
            inertia = InertiaMatrix.IDENTITY;
            if (solid)
            {
                mass = density * height * (float)Math.PI * (float)Math.Pow(radius, 2.0f);
                inertia[0][0] =
                inertia[1][1] = mass * (3.0f * (float)Math.Pow(radius, 3.0f) + (float)Math.Pow(height, 2.0f)) / 12.0f;
                inertia[2][2] = mass * (float)Math.Pow(radius, 2.0f) / 2.0f;
            }
            else
            {
                mass = density * height * (float)Math.PI * (float)Math.Pow(radius, 2.0f);
                inertia[0][0] =
                inertia[1][1] = mass * (6.0f * (float)Math.Pow(radius, 3.0f) + (float)Math.Pow(height, 2.0f)) / 12.0f;
                inertia[2][2] = mass * (float)Math.Pow(radius, 2.0f);
            }
        }


        /*! Return mass and inertia matrix for a capsule of given radius, 
         *	height and density.
         */
        public static void CalcMassPropertiesCapsule(Vector3 startAxis, Vector3 endAxis, float radius,
            float density, bool solid,
            out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            if (extCalcMassPropertiesCapsuleRoutine != null)
            {
                extCalcMassPropertiesCapsuleRoutine(startAxis, endAxis, radius,
                    density, solid,
                    out mass, out volume, out center, out inertia);
                return;
            }
            var height = (startAxis - endAxis).Magnitude();
            center = (startAxis + endAxis) / 2.0f;
            // cylinder + caps, and caps have volume of a sphere
            inertia = InertiaMatrix.IDENTITY;
            if (solid)
            {
                mass = density * (height * (float)Math.PI * (float)Math.Pow(radius, 2.0f) + (4.0f * (float)Math.PI * (float)Math.Pow(radius, 3.0f)) / 3.0f);
                inertia[0][0] = mass * (3.0f * (float)Math.Pow(radius, 3.0f) + (float)Math.Pow(height, 2.0f)) / 12.0f;
                inertia[1][1] = inertia[0][0];
                inertia[2][2] = mass * (float)Math.Pow(radius, 2.0f) / 2.0f;
            }
            else
            {
                mass = density * (height * 2.0f * (float)Math.PI * radius + 2.0f * (float)Math.PI * (float)Math.Pow(radius, 2.0f));
                inertia[0][0] = mass * (6.0f * (float)Math.Pow(radius, 2.0f) + (float)Math.Pow(height, 2.0f)) / 12.0f;
                inertia[1][1] = inertia[0][0];
                inertia[2][2] = mass * (float)Math.Pow(radius, 2.0f);
            }
        }

        /*! Return mass and inertia matrix for a complex polyhedron
         */
        //
        // References
        // ----------
        //
        // Jonathan Blow, Atman J Binstock
        // "How to find the inertia tensor (or other mass properties) of a 3D solid body represented by a triangle mesh"
        // http://number-none.com/blow/inertia/bb_inertia.doc
        //
        // David Eberly
        // "Polyhedral Mass Properties (Revisited)"
        // http://www.geometrictools.com//LibPhysics/RigidBody/Wm4PolyhedralMassProperties.pdf
        //
        // The function is an implementation of the Blow and Binstock algorithm,
        // extended for the case where the polygon is a surface (set parameter
        // solid = False).
        public static void CalcMassPropertiesPolyhedron(IList<Vector3> vertices,
            IList<Triangle> triangles,
            float density, bool solid,
            out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            if (extCalcMassPropertiesPolyhedronRoutine != null)
            {
                extCalcMassPropertiesPolyhedronRoutine((int)vertices.Count, vertices[0],
                    (int)triangles.Count, triangles.Count == 0 ? null : triangles[0],
                    density, solid,
                    out mass, out volume, out center, out inertia);
                return;
            }

            var tri = triangles.Count == 0 ? NifQHull.compute_convex_hull(vertices) : triangles;

            // 120 times the covariance matrix of the canonical tetrahedron
            // (0,0,0),(1,0,0),(0,1,0),(0,0,1)
            // integrate(integrate(integrate(z*z, x=0..1-y-z), y=0..1-z), z=0..1) = 1/120
            // integrate(integrate(integrate(y*z, x=0..1-y-z), y=0..1-z), z=0..1) = 1/60
            var covariance_canonical = new Matrix33(
                2.0f, 1.0f, 1.0f,
                1.0f, 2.0f, 1.0f,
                1.0f, 1.0f, 2.0f);
            var covariances = new List<Matrix44>();
            var masses = new List<float>();
            var centers = new List<Vector3>();

            // for each triangle
            // construct a tetrahedron from triangle + (0,0,0)
            // find its matrix, mass, and center (for density = 1, will be corrected at
            // the end of the algorithm)
            foreach (var itr in tri)
            {
                // Calc vertices
                var vert0 = vertices[itr[0]];
                var vert1 = vertices[itr[1]];
                var vert2 = vertices[itr[2]];

                // construct a transform matrix that converts the canonical tetrahedron
                // into (0,0,0),vert0,vert1,vert2
                var transform_transposed = new Matrix44(new Matrix33(
                    vert0[0], vert0[1], vert0[2],
                    vert1[0], vert1[1], vert1[2],
                    vert2[0], vert2[1], vert2[2]));
                var transform = transform_transposed.Transpose();

                // find the covariance matrix of the transformed tetrahedron/triangle
                if (solid)
                {
                    // we shall be needing the determinant more than once, so
                    // precalculate it
                    var determinant = transform.Determinant();
                    // C' = det(A) * A * C * A^T
                    covariances.Add(transform * covariance_canonical * transform_transposed * determinant);
                    // m = det(A) / 6.0
                    masses.Add(determinant / 6.0f);
                    // find center of gravity of the tetrahedron
                    centers.Add(new Vector3(
                        0.25f * (vert0[0] + vert1[0] + vert2[0]),
                        0.25f * (vert0[1] + vert1[1] + vert2[1]),
                        0.25f * (vert0[2] + vert1[2] + vert2[2])));
                }
                else
                {
                    // find center of gravity of the triangle
                    var com = new Vector3(
                        (vert0[0] + vert1[0] + vert2[0]) / 3.0f,
                        (vert0[1] + vert1[1] + vert2[1]) / 3.0f,
                        (vert0[2] + vert1[2] + vert2[2]) / 3.0f);
                    centers.Add(com);

                    // find mass of triangle
                    // mass is surface, which is half the norm of cross product
                    // of two edges
                    var calc_mass = ((vert1 - vert0) ^ (vert2 - vert0)).Magnitude() / 2.0f;
                    masses.Add(calc_mass);

                    // find covariance at center of this triangle
                    // (this is approximate only as it replaces triangle with point mass
                    // TODO: find better way)
                    var calc_c = new Matrix33(
                        com[0] * com[0], com[0] * com[1], com[0] * com[2],
                        com[1] * com[0], com[1] * com[1], com[1] * com[2],
                        com[2] * com[0], com[2] * com[1], com[2] * com[2]);
                    covariances.Add(new Matrix44(calc_c));
                }
            }
            // accumulate the results
            mass = masses.Accumulate(0.0f, x => x);
            if (mass < 0.0001f)
            {
                // dimension is probably badly chosen
                //raise ZeroDivisionError("mass is zero (consider calculating inertia with a lower dimension)")
                //printf("WARNING: mass is zero");
                mass = 0.0f; volume = 0.0f;
                center = new Vector3();
                inertia = InertiaMatrix.IDENTITY;
                return;
            }

            // weighed average of centers with masses
            center = new Vector3();
            for (var i = 0; i < masses.Length; ++i)
                center += (centers[i] * (masses[i] / mass));

            // add covariances, and correct the values
            var total_covariance = new Matrix44();
            if (solid)
                for (var i = 0; i < covariances.Count; ++i)
                    total_covariance *= covariances[i];

            // translate covariance to center of gravity:
            // C' = C - m * ( x dx^T + dx x^T + dx dx^T )
            // with x the translation vector and dx the center of gravity

            var translate_correction = new Matrix44(new Matrix33(
                center[0] * center[0], center[0] * center[1], center[0] * center[2],
                center[1] * center[0], center[1] * center[1], center[1] * center[2],
                center[2] * center[0], center[2] * center[1], center[2] * center[2]));
            translate_correction *= mass;

            total_covariance = total_covariance - translate_correction;

            // convert covariance matrix into inertia tensor
            var trace = total_covariance[0][0] + total_covariance[1][1] + total_covariance[2][2];
            var trace_matrix = new Matrix44();
            trace_matrix[0][0] = trace_matrix[1][1] = trace_matrix[2][2] = trace;

            // correct for given density
            inertia = new InertiaMatrix((trace_matrix - total_covariance).GetRotation()) * density;
            mass *= density;
        }

        /*! Combine mass properties for a number of objects */
        public static void CombineMassProperties(IList<float> masses,
            IList<float> volumes, IList<Vector3> centers, IList<InertiaMatrix> inertias, IList<Matrix44> transforms,
            ref float mass, ref float volume, ref Vector3 center, ref InertiaMatrix inertia)
        {
            if (extCombineMassPropertiesRoutine != null)
            {
                extCombineMassPropertiesRoutine((int)masses.Count,
                    masses[0], volumes[0], centers[0], inertias[0], transforms[0],
                    out mass, out volume, out center, out inertia);
                return;
            }
            for (var i = 0; i < masses.Count; ++i)
                mass += masses[i];
            // TODO: doubt this inertia calculation is even remotely close
            for (var i = 0; i < masses.Count; ++i)
            {
                center += centers[i] * (masses[i] / mass);
                inertia *= inertias[i];
            }
        }
    }
}