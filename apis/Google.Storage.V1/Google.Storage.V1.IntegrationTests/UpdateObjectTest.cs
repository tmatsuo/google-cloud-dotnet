﻿// Copyright 2016 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using Xunit;

namespace Google.Storage.V1.IntegrationTests
{
    using System.IO;
    using static TestHelpers;

    [Collection(nameof(StorageFixture))]
    public class UpdateObjectTest
    {
        private readonly StorageFixture _fixture;

        public UpdateObjectTest(StorageFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Success()
        {
            var client = _fixture.Client;
            var obj = client.UploadObject(_fixture.SingleVersionBucket, GenerateName(), null,
                new MemoryStream(_fixture.SmallContent), new UploadObjectOptions { Projection = Projection.Full });
            obj.Metadata = new Dictionary<string, string> { { "key", "value" } };
            var updated = client.UpdateObject(obj);
            Assert.Equal("value", updated.Metadata["key"]);
        }

        [Fact]
        public void NoAcl()
        {
            // Common way this would happen... we're not getting the full projection, so we have no ACLs.
            var client = _fixture.Client;
            var obj = client.UploadObject(_fixture.SingleVersionBucket, GenerateName(), null,
                new MemoryStream(_fixture.SmallContent));
            obj.Metadata = new Dictionary<string, string> { { "key", "value" } };
            Assert.Throws<ArgumentException>(() => client.UpdateObject(obj));
        }
    }
}
