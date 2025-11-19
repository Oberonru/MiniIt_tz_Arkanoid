using System;
using UnityEngine;

namespace Infrastructure.SO
{
    public class ScriptableObjectIdentity : ScriptableObject
    {
        public string ID => _id;
        private string _id = Guid.NewGuid().ToString();
    }
}