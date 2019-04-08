using System;
using System.Collections.Generic;
using System.Text;

using DWR.StateMod;

namespace cdss.statemod.app
{
    public class StateModRunner
    {
        /*
	     * StateMod dataset used by the simulator.
	     */
        private StateMod_DataSet dataset = null;

        /*
         * Constructor.
         * @param dataset existing dataset to use for simulation.
         */
        public StateModRunner(StateMod_DataSet dataset)
        {
            this.dataset = dataset;
        }

        /*
	     * Run the baseflow mode.
	     */
        public void runBaseflows()
        {
            Console.WriteLine("Running baseflow mode.");
        }

        /*
	     * Run the check.
	     */
        public void runCheck()
        {
            Console.WriteLine("Running check mode.");
        }

        /*
         * Run the simulation.
         */
        public void runSimulation()
        {
            Console.WriteLine("Running simulation.");
            // TODO smalers 2019-03-14 need to implement logic
        }
    }
}
