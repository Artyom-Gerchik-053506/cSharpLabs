using System;

namespace LAB3
{
    public class Wada // World Anti-Doping Agency
    {
        public void checkForDoping(ITakeBlood test)
        {
            Blood blood = test.takeBlood();
            if (blood.ID % 10 > 7)
            {
                throw new Exception("Doping Detected");
            }
        }
    }
}