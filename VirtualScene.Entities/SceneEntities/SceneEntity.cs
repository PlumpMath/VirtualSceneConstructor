﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// An entity in the scene
    /// </summary>
    public abstract class SceneEntity<T> : ISceneEntity, INotifyPropertyChanged
        where T: SceneElement
    {
        /// <summary>
        /// The inctance of the concrete type <see cref="T" />.
        /// </summary>
        [XmlIgnore]
        public T SceneElement { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneEntity{T}" />
        /// </summary>
        /// <param name="description">The description of the <see cref="ISceneEntity" /></param>
        protected SceneEntity(string description)
        {
            Id = Guid.NewGuid();
            Description = description;
        }

        /// <summary>
        /// The is of the <see cref="ISceneEntity" />
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Representation of the <see cref="ISceneEntity" /> in the scene.
        /// </summary>
        [XmlIgnore]
        public SceneElement Geometry
        {
            get
            {
                if (SceneElement != null) 
                    return SceneElement;
                SceneElement = CreateGeometry();
                UpdateFields(SceneElement);
                return SceneElement;
            }
            set
            {
                SceneElement = value as T;
                UpdateFields(SceneElement);
            }
        }

        /// <summary>
        /// Update private data after a new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="T" />.</param>
        protected abstract void UpdateFields(T sceneElement);

        /// <summary>
        /// Create the <see cref="SharpGL.SceneGraph.Core.SceneElement" /> specific for each type of <see cref="ISceneEntity" />
        /// </summary>
        /// <returns>Returns a new instance of <see cref="T" /></returns>
        protected abstract T CreateGeometry();

        /// <summary>
        /// The name of the entity in the scene
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        [XmlIgnore]
        public string Description { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var sceneEntity = (SceneEntity<T>)obj;
            return string.Equals(Name, sceneEntity.Name) 
                   && GeometryEqualityHelper.SceneElementEqual(Geometry, sceneEntity.Geometry);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke the event <see cref="PropertyChanged" />.
        /// </summary>
        /// <param name="propertyName">(Optional) The name of the changed property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}