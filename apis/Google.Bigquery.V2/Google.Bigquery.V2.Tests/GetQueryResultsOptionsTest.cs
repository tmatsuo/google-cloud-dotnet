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

using Google.Apis.Bigquery.v2;
using System;
using Xunit;
using static Google.Apis.Bigquery.v2.JobsResource;

namespace Google.Bigquery.V2.Tests
{
    public class GetQueryResultsOptionsTest
    {
        [Fact]
        public void ModifyRequest()
        {
            var options = new GetQueryResultsOptions
            {
                StartIndex = 10,
                PageSize = 25,
                Timeout = TimeSpan.FromSeconds(5)
            };
            GetQueryResultsRequest request = new GetQueryResultsRequest(new BigqueryService(), "project", "job");
            options.ModifyRequest(request);
            Assert.Equal(10UL, request.StartIndex);
            Assert.Equal(25, request.MaxResults);
            Assert.Equal(5 * 1000, request.TimeoutMs);
        }
    }
}
