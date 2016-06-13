using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonMasterEngine.DungeonContent.Items.GrabableItems;
using DungeonMasterEngine.Interfaces;
using Microsoft.Xna.Framework;
using FloorActuator = DungeonMasterEngine.DungeonContent.Actuators.Wall.FloorSensors.FloorActuator;

namespace DungeonMasterEngine.DungeonContent.Tiles
{

    public class ActuatorFloorTileSide : FloorTileSide
    {
        public FloorActuator Actuator { get; }


        public override void OnObjectLeft(object localizable)
        {
            Actuator.Trigger(localizable, subItems, false);
            base.OnObjectLeft(localizable);
        }

        public override void OnObjectEntered(object localizable, bool addToSpacd = true)
        {
            Actuator.Trigger(localizable, subItems, true);
            base.OnObjectEntered(localizable);
        }

        public ActuatorFloorTileSide(FloorActuator actuator, bool randomDecoration, MapDirection face, IEnumerable<IGrabableItem> topLeftItems, IEnumerable<IGrabableItem> topRightItems, IEnumerable<IGrabableItem> bottomLeftItems, IEnumerable<IGrabableItem> bottomRightItems) : base(randomDecoration, face, topLeftItems, topRightItems, bottomLeftItems, bottomRightItems)
        {
            Actuator = actuator;
        }
    }

    public class FloorTileSide : TileSide, IEnumerable<object>
    {
        public event EventHandler SubItemsChaned;


        protected readonly List<object> subItems = new List<object>();
        public IReadOnlyList<FloorItemStorage> Spaces { get; }

        public FloorTileSide(bool randomDecoration, MapDirection face, IEnumerable<IGrabableItem> topLeftItems, IEnumerable<IGrabableItem> topRightItems, IEnumerable<IGrabableItem> bottomLeftItems, IEnumerable<IGrabableItem> bottomRightItems) : base(face, randomDecoration)
        {
            Spaces = new[]
            {
                new FloorItemStorage(new Point(-1,1),  topLeftItems),
                new FloorItemStorage(new Point(1,1),topRightItems),
                new FloorItemStorage(new Point(-1,-1),bottomLeftItems),
                new FloorItemStorage(new Point(1,-1),bottomRightItems),
            };

            subItems.AddRange(Spaces.SelectMany(s => s.Items));

            foreach (var space in Spaces)
            {
                space.ItemAdding += (sender, item) => OnObjectEntered(item, addToSpacd: false);
                space.ItemRemoving += (s, item) => OnObjectLeft(item);
            }
        }

        public virtual void OnObjectEntered(object localizable, bool addToSpacd = true)
        {
            if (subItems.Contains(localizable))
                throw new InvalidOperationException("item already in collection");

            subItems.Add(localizable);

            if (addToSpacd)
            {
                var grabable = localizable as GrabableItem;
                if (grabable != null)
                {
                    Spaces.First().AddItem(grabable);
                }
            }

            SubItemsChaned?.Invoke(this, new EventArgs());
        }

        public virtual void OnObjectLeft(object localizable)
        {

            if (!subItems.Contains(localizable))
                throw new InvalidOperationException("item is not in collection");

            subItems.Remove(localizable);

            var grabable = localizable as GrabableItem;
            if (grabable != null)
            {
                foreach (var storage in Spaces)
                    storage.RemoveItem(grabable, false);
            }
            SubItemsChaned?.Invoke(this, new EventArgs());
        }

        public IEnumerator<object> GetEnumerator()
        {
            return subItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}