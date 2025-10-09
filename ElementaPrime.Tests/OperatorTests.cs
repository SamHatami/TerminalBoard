using ElementaPrime.Core;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Graphs.Conduits;
using ElementaPrime.Core.Graphs.Management;
using ElementaPrime.Core.Graphs.Operators;
using ElementaPrime.Providers.Standard;
using ElementaPrime.Core.Graphs.Excecution;

namespace ElementaPrime.Tests
{
    
    //TODO: Tests needs to be rewritten to use the new type of terminals

    public class OperatorTests
    {
        private VaultService _vaultService;
        private BaseMath _baseMath;
        private VectorMath _vectorMath;
        private readonly OperatorFactory _operatorFactory;

        public OperatorTests()
        {
            _operatorFactory = new OperatorFactory();
            
            Setup();
        }

        private void Setup()
        {
            //Create and register providers
            _vaultService = new VaultService();
            _baseMath = new BaseMath();
            _vectorMath = new VectorMath();

            _operatorFactory.RegisterProviderMethods(_vaultService);
            _operatorFactory.RegisterProviderMethods(_baseMath);
            _operatorFactory.RegisterProviderMethods(_vectorMath);

        }
        [Fact]
        public void SimpleTwoOperatorConnectionTest()
        {
            //act
            var conduitManager = new ConduitManager();
            var graphManager = new GraphManager(conduitManager, _operatorFactory);
            var operatorInfos = graphManager.GetAllProviderOperatorsInfos();

            var valueOperator = new ValueOperator<long>(){Value = 1235901835};

            var getFileOperatorInfo = operatorInfos.SingleOrDefault(info =>
                info.OperatorId.Value == "ElementaPrime.Tests.VaultService.GetFile(long):VaultFile");

            var getFileOperator =graphManager.GetProviderOperator(getFileOperatorInfo);

            var typedValue = new TypedValue<long>("", new Guid()) { Value = valueOperator.Value };
            
            if(!ConduitValidator.Validate(valueOperator.Outputs[0], getFileOperator.InputSockets[0]))
                Assert.Fail("Connection between dataports are not valid");
            
            var valueConduit =
                new ConduitConnection(valueOperator.Outputs[0], getFileOperator.InputSockets[0], typedValue);

            graphManager.ConnectOperators(valueConduit,valueOperator,getFileOperator);

            var operationResult = getFileOperator.Execute();

            Assert.True(operationResult.Equals(OperationResult.Success));
        }

        [Fact]
        public void OperatorFactoryInfoTests()
        {
            var operatorInfos = _operatorFactory.GetAllProviderOperatorInfos();
        }
    }
}