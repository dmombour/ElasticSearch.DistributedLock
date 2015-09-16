using System;
using ElasticSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using NSubstitute;

namespace ElasticSearch.DistributedLock.Tests
{
    [TestClass]
    public class ElasticSearchDistributedLockTests
    {
        [TestMethod]
        public void ThrowArgumentErrorOnEmptyString()
        {
            var esClient = Substitute.For<IElasticClient>();

            bool error = false;
            try
            {
                var dLock = new ElasticSearchDistributedLock("", esClient);
            }
            catch (Exception)
            {
                error = true;
            }

            Assert.IsTrue(error);
        }

        [TestMethod]
        public void AquireReturnsTrueWhenResponseIsValid()
        {
            var esClient = Substitute.For<IElasticClient>();
            esClient.Update<object>(u => u.Script("")).ReturnsForAnyArgs(call =>
               {
                   var response = Substitute.For<IUpdateResponse>();
                   response.IsValid.Returns(true);
                   return response;

               });

            var dLock = new ElasticSearchDistributedLock("Test", esClient);
            Assert.IsTrue(dLock.Aquire());
        }
        [TestMethod]
        public void AquireReturnsFalseWhenResponseIsValid()
        {
            var esClient = Substitute.For<IElasticClient>();
            esClient.Update<object>(u => u.Script("")).ReturnsForAnyArgs(call =>
            {
                var response = Substitute.For<IUpdateResponse>();
                response.IsValid.Returns(false);
                return response;

            });

            var dLock = new ElasticSearchDistributedLock("Test", esClient);
            Assert.IsFalse(dLock.Aquire());
        }
        [TestMethod]
        public void AquireReturnsFalseWhenErrorOccurs()
        {
            var esClient = Substitute.For<IElasticClient>();
            esClient.Update<object>(u => u.Script("")).ReturnsForAnyArgs(call =>
            {
                throw new Exception("error");
                //var response = Substitute.For<IUpdateResponse>();
                //response.IsValid.Returns(false);
                //return response;

            });

            var dLock = new ElasticSearchDistributedLock("Test", esClient);
            Assert.IsFalse(dLock.Aquire());
        }

        [TestMethod]
        public void CanReleaseWithoutAquire()
        {
            var esClient = Substitute.For<IElasticClient>();
            var dLock = new ElasticSearchDistributedLock("Test", esClient);
            Assert.IsTrue(dLock.Release());
        }

        [TestMethod]
        public void CanReleaseAfterAquire()
        {
            var esClient = Substitute.For<IElasticClient>();
            esClient.Delete("", "", "").ReturnsForAnyArgs(call =>
            {
                var response = Substitute.For<IDeleteResponse>();
                response.IsValid.Returns(true);
                return response;
            });

            esClient.Update<object>(u => u.Script("")).ReturnsForAnyArgs(call =>
            {
                var response = Substitute.For<IUpdateResponse>();
                response.IsValid.Returns(true);
                return response;

            });

            var dLock = new ElasticSearchDistributedLock("Test", esClient);
            Assert.IsTrue(dLock.Aquire());
            Assert.IsTrue(dLock.Release());
        }
    }
}
