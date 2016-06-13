using System.Collections.Generic;
using DungeonMasterEngine.DungeonContent.Entity.Attacks;
using DungeonMasterEngine.DungeonContent.Entity.BodyInventory.@base;
using DungeonMasterEngine.DungeonContent.GroupSupport;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonMasterEngine.DungeonContent.Items.GrabableItems.Factories
{
    public abstract class GrabableItemFactoryBase : IGrabableItemFactoryBase
    {
        private readonly HashSet<IAttackFactory> combos;
        private readonly HashSet<IStorageType> locations;

        public Texture2D Texture { get; }
        public string Name { get; }
        public int Weight { get; }
        public IEnumerable<IAttackFactory> AttackCombo => combos;
        public IEnumerable<IStorageType> CarryLocation => locations;

        protected GrabableItemFactoryBase(string name, int weight, IEnumerable<IAttackFactory> attackCombo, IEnumerable<IStorageType> carryLocation, Texture2D texture)
        {
            Name = name;
            Weight = weight;
            Texture = texture;
            combos = new HashSet<IAttackFactory>(attackCombo);
            locations = new HashSet<IStorageType>(carryLocation);
        }

        public bool CanBeStoredIn(IStorageType storage) => locations.Contains(storage);

        public abstract IGrabableItem Create();
    }
}