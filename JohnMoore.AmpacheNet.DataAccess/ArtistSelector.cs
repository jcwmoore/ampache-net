//
// ArtistSelector.cs
//
// Author:
//       John Moore <jcwmoore@gmail.com>
//
// Copyright (c) 2010 John Moore
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using JohnMoore.AmpacheNet.Entities;

namespace JohnMoore.AmpacheNet.DataAccess
{
    internal class ArtistSelector : AmpacheSelectorBase<AmpacheArtist>
    {
        private const string NODE_NAME = @"artist";
        private const string ACTION = @"artists";
        private const string TAG_ACTION = @"tag_artists";
        private static Dictionary<Type, string> MAP;

        static ArtistSelector ()
        {
            MAP = new Dictionary<Type, string>();
            MAP.Add(typeof(Tag), TAG_ACTION);
        }

        public ArtistSelector (Handshake handshake, IEntityFactory<AmpacheArtist> factory) : base (handshake, factory)
        {}

        #region implemented abstract members of JohnMoore.AmpacheNet.DataAccess.AmpacheSelectorBase[JohnMoore.AmpacheNet.DataAccess.AmpacheSong]
        protected override string SelectAllMethod { get { return ACTION; } }

        protected override string XmlNodeName { get { return NODE_NAME; } }

        protected override Dictionary<Type, string> SelectMethodMap { get { return MAP; } }

		protected override string SelectSingleMethod { get { return NODE_NAME; } }
        #endregion

    }
}
