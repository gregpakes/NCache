// Copyright (c) 2018 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Alachisoft.NCache.Web.Command
{
    internal sealed class RemoveGroupCommand : CommandBase
    {
        private Alachisoft.NCache.Common.Protobuf.RemoveGroupCommand _removeGroupCommand;
        private int _methodOverload;

        public RemoveGroupCommand(string group, string subGroup, int methodOverload)
        {
            base.name = "RemoveGroupCommand";
            _removeGroupCommand = new Alachisoft.NCache.Common.Protobuf.RemoveGroupCommand();
            _removeGroupCommand.group = group;
            _removeGroupCommand.subGroup = subGroup;
            _removeGroupCommand.requestId = RequestId;
            _methodOverload = methodOverload;
        }

        internal override CommandType CommandType
        {
            get { return CommandType.REMOVE_GROUP; }
        }

        internal override RequestType CommandRequestType
        {
            get { return RequestType.NonKeyBulkWrite; }
        }

        internal override bool IsKeyBased
        {
            get { return false; }
        }

        protected override void CreateCommand()
        {
            base._command = new Alachisoft.NCache.Common.Protobuf.Command();
            base._command.requestID = base.RequestId;
            base._command.removeGroupCommand = _removeGroupCommand;
            base._command.type = Alachisoft.NCache.Common.Protobuf.Command.Type.REMOVE_GROUP;
            base._command.clientLastViewId = base.clientLastViewId;
            base._command.MethodOverload = _methodOverload;
        }
    }
}