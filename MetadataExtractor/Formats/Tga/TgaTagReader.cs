﻿using MetadataExtractor.IO;
using System.IO;

namespace MetadataExtractor.Formats.Tga
{
    internal struct TgaTagInfo
    {
        public short Id;
        public int Offset;
        public int Size;
    }

    internal sealed class TgaTagReader : TgaReader<TgaTagInfo[]>
    {
        protected override TgaTagInfo[] Extract(Stream stream, int _)
        {
            var reader = new SequentialStreamReader(stream, isMotorolaByteOrder: false);
            var count = reader.GetUInt16();
            var tags = new TgaTagInfo[count];
            for (int i = 0; i < count; i++)
                tags[i] = GetTag(reader);
            return tags;
        }

        private static TgaTagInfo GetTag(SequentialReader reader)
        {
            return new TgaTagInfo
            {
                Id = reader.GetInt16(),
                Offset = reader.GetInt32(),
                Size = reader.GetInt32()
            };
        }
    }
}
