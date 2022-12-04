﻿using BloodLoop.Application.Providers.RckikKatowice;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BloodLoop.Application.Tests.Providers.RckikKatowice
{
    public class RckikKatowiceClientTests
    {
        string response =
@"[{""id"":111,""date"":""2022-02-07T08:40:43"",""date_gmt"":""2022-02-07T07:40:43"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=111""},""modified"":""2022-08-16T08:10:58"",""modified_gmt"":""2022-08-16T06:10:58"",""slug"":""0-rh-2"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/0-rh-2\/"",""title"":{""rendered"":""0 Rh+""},""content"":{""rendered"":"""",""protected"":false},""template"":"""",""acf"":{""level"":""95""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/111""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=111""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":112,""date"":""2022-02-07T08:00:55"",""date_gmt"":""2022-02-07T07:00:55"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=112""},""modified"":""2022-08-16T08:09:52"",""modified_gmt"":""2022-08-16T06:09:52"",""slug"":""0-rh"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/0-rh\/"",""title"":{""rendered"":""0 Rh-""},""content"":{""rendered"":"""",""protected"":false},""template "":"""",""acf"":{""level"":""24""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/112""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=112""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":109,""date"":""2019-02-14T10:05:12"",""date_gmt"":""2019-02-14T10:05:12"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=109""},""modified"":""2022-08-05T09:13:19"",""modified_gmt"":""2022-08-05T07:13:19"",""slug"":""ab-2"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/ab-2\/"",""title"":{""rendered"":""AB Rh-""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""100""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/109""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=109""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":108,""date"":""2019-02-14T10:05:02"",""date_gmt"":""2019-02-14T10:05:02"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=108""},""modified"":""2022-08-05T09:14:31"",""modified_gmt"":""2022-08-05T07:14:31"",""slug"":""ab"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/ab\/"",""title"":{""rendered"":""AB Rh+""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""100""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/108""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=108""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":107,""date"":""2019-02-14T10:04:46"",""date_gmt"":""2019-02-14T10:04:46"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=107""},""modified"":""2022-08-16T08:09:24"",""modified_gmt"":""2022-08-16T06:09:24"",""slug"":""b-2"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/b-2\/"",""title"":{""rendered"":""B Rh-""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""95""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/107""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=107""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":106,""date"":""2019-02-14T10:04:36"",""date_gmt"":""2019-02-14T10:04:36"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=106""},""modified"":""2022-08-05T09:14:22"",""modified_gmt"":""2022-08-05T07:14:22"",""slug"":""b"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/b\/"",""title"":{""rendered"":""B Rh+""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""100""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/106""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=106""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":105,""date"":""2019-02-14T10:04:15"",""date_gmt"":""2019-02-14T10:04:15"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=105""},""modified"":""2022-08-16T08:08:44"",""modified_gmt"":""2022-08-16T06:08:44"",""slug"":""a-2"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/a-2\/"",""title"":{""rendered"":""A Rh-""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""87""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/105""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=105""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}},{""id"":100,""date"":""2019-02-14T10:00:40"",""date_gmt"":""2019-02-14T10:00:40"",""guid"":{""rendered"":""https:\/\/rckik-katowice.pl\/wp\/?post_type=krew&#038;p=100""},""modified"":""2022-08-16T08:10:25"",""modified_gmt"":""2022-08-16T06:10:25"",""slug"":""a"",""status"":""publish"",""type"":""krew"",""link"":""https:\/\/rckik-katowice.pl\/wp\/krew\/a\/"",""title"":{""rendered"":""A Rh+""},""content"":{""rendered"":"""",""protected "":false},""template "":"""",""acf"":{""level"":""77""},""_links"":{""self"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew\/100""}],""collection"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/krew""}],""about"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/types\/krew""}],""wp:attachment"":[{""href"":""https:\/\/rckik-katowice.pl\/wp\/wp-json\/wp\/v2\/media?parent=100""}],""curies"":[{""name"":""wp"",""href"":""https:\/\/api.w.org\/{rel}"",""templated"":true}]}}]";

        [Fact]
        public void BloodLevelsResponse_ShouldParse()
        {
            var parsedBloodLevels = JsonSerializer.Deserialize<List<RK_BloodRS>>(response);

            parsedBloodLevels.Count.Should().Be(8);
            var firstBloodLevel = parsedBloodLevels.First();
            firstBloodLevel.Date.Date.Should().BeCloseTo(DateTime.Parse("2022-08-16T00:00:00"), TimeSpan.FromHours(12));
            firstBloodLevel.Slug.Should().NotBeNull().And.Be("0-rh-2");
            firstBloodLevel.Acf.Should().NotBeNull();
            firstBloodLevel.Acf.Level.Should().NotBeNull().And.Be("95");
        }
    }
}