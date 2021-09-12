namespace MonoGame.Extras.Ecs
{
    using DotNet.Extras.Collections;
    using System;

    public class Aspect
    {
        internal Aspect()
        {
            AllSet = new BitArray64();
            OneSet = new BitArray64();
            NotSet = new BitArray64();
        }

        public BitArray64 AllSet;
        public BitArray64 OneSet;
        public BitArray64 NotSet;

        public static AspectBuilder All(params Type[] componentTypes)
        {
            return new AspectBuilder().All(componentTypes);
        }

        public static AspectBuilder One(params Type[] componentTypes)
        {
            return new AspectBuilder().One(componentTypes);
        }

        public static AspectBuilder Not(params Type[] componentTypes)
        {
            return new AspectBuilder().Not(componentTypes);
        }

        public bool IsInterested(BitArray64 componentTypes)
        {
            // ALL
            if(AllSet.Bits != 0 && (componentTypes.Bits & AllSet.Bits) != AllSet.Bits)
            {
                return false;
            }

            // NOT
            if(NotSet.Bits != 0 && (componentTypes.Bits & NotSet.Bits) != 0)
            {
                return false;
            }

            // ONE
            if(OneSet.Bits != 0 && (componentTypes.Bits & OneSet.Bits) == 0)
            {
                return false;
            }

            return true;
        }

    }
}
