using System;
using UnityEngine;

namespace CockroachRunner
{
    [Serializable]
    public class CockroachVariants
    {
        [SerializeField] private Cockroach[] variants;

        public Cockroach[] Variants => variants;

        public Cockroach RandomVariant => variants[UnityEngine.Random.Range(0, variants.Length - 1)];
    }
}