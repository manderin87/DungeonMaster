using System.Collections.Generic;
using DungeonMasterEngine.Builders.ItemCreator;
using DungeonMasterEngine.DungeonContent.Entity.Actions.Factories;
using DungeonMasterEngine.DungeonContent.Entity.BodyInventory.Base;
using DungeonMasterEngine.DungeonContent.GrabableItems.Initializers;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonMasterEngine.DungeonContent.GrabableItems.Factories
{
    public class MiscItemFactory : GrabableItemFactoryBase
    {
        public MiscItemFactory(string name, int weight, IEnumerable<IActionFactory> attackCombo, IEnumerable<IStorageType> carryLocation, Texture2D texture) : base(name, weight, attackCombo, carryLocation,texture)
        {
            
        }

        public Miscellaneous Create<TItemInitiator>(TItemInitiator initiator) where TItemInitiator : IMiscInitializer
        {
            return new Miscellaneous(initiator, this);
        }

        public override IGrabableItem Create()
        {
            return Create(new MiscInitializer());
        }
    }
}