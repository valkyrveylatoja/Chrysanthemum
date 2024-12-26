using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMANDS
{
    public abstract class CMD_DatabaseExtension // Abstract meaning it cannot be instanced
    {
        public static void Extend(CommandDatabase database) { }
    }
}