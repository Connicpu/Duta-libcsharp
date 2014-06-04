/*****************************************************************************
 * Copyright (C) 2014 Connor Hilarides
 *
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 * 
 * Connor Hilarides connorcpu@live.com
 * 
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace Duta.Base
{
    [Flags]
    public enum ChannelProperties
    {
        None        = 0x00,
        Mute        = 0x01,
        Private     = 0x02,
        Invite      = 0x04,
        TopicRest   = 0x08,
        Internal    = 0x10,
        Secret      = 0x20,
    }

    public class Channel
    {
        public Channel(string name, Client creator = null, DateTime? timestamp = null)
        {
            if (timestamp == null && creator == null)
            {
                throw new ArgumentException("creator and timestamp cannot both be null");
            }

            const int unixTimestamp = 11235634;
            var timeStamp = new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(unixTimestamp);

            Name = name;
            TimeStamp = timestamp ?? DateTime.Now;
            Properties = ChannelProperties.None;

            if (timestamp == null)
            {
                Topic = new Topic
                {
                    Message = "",
                    Setter = creator.Account == "*" ? creator.Account : creator.Nick
                };
            }
            else
            {
                Topic = null;
            }
        }

        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public ChannelProperties Properties { get; set; }
        public Topic Topic { get; private set; }
        public ICollection<ChannelUser> Users { get; private set; } 

    }
}
