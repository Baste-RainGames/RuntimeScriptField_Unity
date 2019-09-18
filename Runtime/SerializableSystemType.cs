﻿// Simple helper class that allows you to serialize System.Type objects.
// Use it however you like, but crediting or even just contacting the author would be appreciated (Always
// nice to see people using your stuff!)
//
// (Originally) written by Bryan Keiren (http://www.bryankeiren.com)

using System;
using UnityEngine;
using Object = System.Object;

namespace RuntimeScriptField.Inner
{
    [Serializable]
    public class SerializableSystemType
    {
        [SerializeField] private string m_Name;
        public string Name { get { return m_Name; } }

        [SerializeField] private string m_AssemblyQualifiedName;
        public string AssemblyQualifiedName { get { return m_AssemblyQualifiedName; } }

        private Type m_SystemType;
        public Type SystemType
        {
            get
            {
                if (m_SystemType == null)
                {
                    GetSystemType();
                }
                return m_SystemType;
            }
        }

        private void GetSystemType()
        {
            m_SystemType = string.IsNullOrEmpty(m_AssemblyQualifiedName) ? null : Type.GetType(m_AssemblyQualifiedName);
        }

        public SerializableSystemType(Type _SystemType)
        {
            if (_SystemType == null)
                throw new ArgumentNullException("_SystemType");
            m_SystemType = _SystemType;
            m_Name = _SystemType.Name;
            m_AssemblyQualifiedName = _SystemType.AssemblyQualifiedName;
        }

        public override bool Equals(Object obj)
        {
            SerializableSystemType temp = obj as SerializableSystemType;
            if ((object) temp == null)
            {
                return false;
            }
            return Equals(temp);
        }

        public bool Equals(SerializableSystemType _Object)
        {
            if (_Object == null)
                return false;
            return SystemType.Equals(_Object.SystemType);
        }

        public static bool operator ==(SerializableSystemType a, SerializableSystemType b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(SerializableSystemType a, SerializableSystemType b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return SystemType.GetHashCode();
        }

        public static implicit operator SerializableSystemType(Type type)
        {
            return new SerializableSystemType(type);
        }

        public static implicit operator Type(SerializableSystemType type)
        {
            return type.SystemType;
        }
    }
}