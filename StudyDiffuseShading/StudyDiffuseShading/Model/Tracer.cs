﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Lighting;
using StudyDiffuseShading.Model.Sampler;

namespace StudyDiffuseShading.Model {
    public class Tracer {
        private Construction construction;
        private ILight environment;
        private IHemispherecalSampler sampler;
        private int maxDepth;
        private int depth;

        public Tracer(Construction construction, ILight environment, IHemispherecalSampler sampler, int maxDepth) {
            this.construction = construction;
            this.environment = environment;
            this.sampler = sampler;
            this.maxDepth = maxDepth;
            this.depth = 0;
        }


        public Vector3D traceRay(Ray ray) {
            if (maxDepth < depth)
                return Constant.BLACK;

            double nearest;
            Triangle target;
            Collision collision;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target, out collision))
                return environment.l();

            depth++;
            Vector3D result = target.matter.shade(this, sampler, collision);
            depth--;
            return result;
        }
    }
}
