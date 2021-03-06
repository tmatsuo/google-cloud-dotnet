﻿// Copyright 2015 Google Inc. All Rights Reserved.
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

using Google.Api.Gax.Rest;
using Google.Apis.Requests;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Storage.V1
{
    // ListBuckets methods on StorageClient
    public sealed partial class StorageClientImpl : StorageClient
    {
        private sealed class BucketPageManager : IPageManager<BucketsResource.ListRequest, Buckets, Bucket>
        {
            internal static readonly BucketPageManager Instance = new BucketPageManager();
            public string GetNextPageToken(Buckets response) => response.NextPageToken;
            public IEnumerable<Bucket> GetResources(Buckets response) => response.Items;
            public void SetPageSize(BucketsResource.ListRequest request, int pageSize) => request.MaxResults = pageSize;
            public void SetPageToken(BucketsResource.ListRequest request, string pageToken) => request.PageToken = pageToken;
        }

        /// <inheritdoc />
        public override IPagedAsyncEnumerable<Buckets, Bucket> ListBucketsAsync(
            string projectId, ListBucketsOptions options = null)
        {
            GaxRestPreconditions.CheckNotNull(projectId, nameof(projectId));
            return new PagedAsyncEnumerable<BucketsResource.ListRequest, Buckets, Bucket>(
                () => CreateListBucketsRequest(projectId, options), BucketPageManager.Instance);
        }

        /// <inheritdoc />
        public override IPagedEnumerable<Buckets, Bucket> ListBuckets(string projectId, ListBucketsOptions options = null)
        {
            GaxRestPreconditions.CheckNotNull(projectId, nameof(projectId));
            return new PagedEnumerable<BucketsResource.ListRequest, Buckets, Bucket>(
                () => CreateListBucketsRequest(projectId, options), BucketPageManager.Instance);
        }

        private BucketsResource.ListRequest CreateListBucketsRequest(string projectId, ListBucketsOptions options)
        {
            var request = Service.Buckets.List(projectId);
            options?.ModifyRequest(request);
            return request;
        }
    }
}
