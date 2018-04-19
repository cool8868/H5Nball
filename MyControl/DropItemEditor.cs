﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Games.MyControl
{
    public class DropItemEditor : CollectionEditor
    {
        // Methods
        public DropItemEditor(Type type)
            : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(FieldParam);
        }
    }

 

}
