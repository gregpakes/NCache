﻿// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Alachisoft.NCache.Runtime.Aggregation;
using System.Collections;
using Alachisoft.NCache.Common.Enum;

namespace Alachisoft.NCache.Web.Aggregation
{
    /// <summary>
    /// Implements built in Integer type aggregator. 
    /// </summary>
    [Serializable]
    public class IntegerAggregator : IAggregator
    {
        AggregateFunctionType aggregateType;
        object result = 0;

        public IntegerAggregator(AggregateFunctionType type)
        {
            this.aggregateType = type;
        }

        /// <summary>
        /// Performs given logic of aggregate on local node like combiner. 
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>Retuns aggregated result.</returns>
        public object Aggregate(object value)
        {
            if (value is ICollection)
                return Calculate((ICollection)value, false);
            else
                return null;
        }

        /// <summary>
        /// Performs given logic of aggregate on all server nodes like Reduce phase operation. 
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>Retuns aggregated result.</returns>
        public object AggregateAll(object value)
        {
            if (value is ICollection)
                return Calculate((ICollection)value, true);
            else
                return null;
        }

        private object Calculate(ICollection value, bool calculate)
        {
            switch (aggregateType)
            {
                case AggregateFunctionType.SUM:
                    object res = Calculation.Sum(value, DataType.INTEGER);
                    if (res == null) result = 0;
                    else result = (int)res;
                    break;
                case AggregateFunctionType.AVG:
                    result = Calculation.Avg(value, DataType.INTEGER, calculate);
                    break;
                case AggregateFunctionType.MIN:
                    result = (int)Calculation.Min(value, DataType.INTEGER);
                    break;
                case AggregateFunctionType.MAX:
                    result = (int)Calculation.Max(value, DataType.INTEGER);
                    break;
            }

            return result;
        }
    }
}
