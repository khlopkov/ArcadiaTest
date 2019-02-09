using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcadiaTest.BusinessLayer.DTO
{
    public class Timetable
    {
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> values)
        {
            if (values.Count() == 1)
                return new[] {values.ToArray()};

            return values.SelectMany(v => GetPermutations(values.Except(new[] {v}).ToArray()),
                (v, p) => new[] {v}.Concat(p).ToArray());
        }

        private List<Workday> _workdays;
        private IEnumerable<CookDTO> _workers;
        private IEnumerable<IEnumerable<DayGraphic>> Workdays
        {
            get => this._workdays.Select(c => c.Workers);
        }

        public Timetable(IEnumerable<CookDTO> workers)
        {
            this._workers = workers;
            this._workdays = initWorkdays();
        }

        private static List<Workday> initWorkdays()
        {
            var daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var workdays = new List<Workday>(daysInCurrentMonth);
            for (var i = 0; i < daysInCurrentMonth; i++)
            {
                workdays.Add(new Workday());
            }

            return workdays;
        }

        public IEnumerable<IEnumerable<DayGraphic>> GetTimetable()
        {
            var tempWorkdays = initWorkdays();
            foreach (var worker in this._workers)
            {
                int workweek;
                int workdays;
                if (worker.Workdays == CookDTO.WorkdaysType.Two)
                {
                    workweek = 4;
                    workdays = 2;
                }
                else
                {
                    workweek = 7;
                    workdays = 5;
                }

                var maxResultFunc = (short)0;
                var bestOffset = 0;
                var resultFuncs = new short?[tempWorkdays.Count];
                for (var offset = 0; offset < workweek; offset++)
                {
                    var resultFunc = (short) 0;
                    for (var day = 0; day < tempWorkdays.Count; day++)
                    {
                        if ((workweek + day - offset) % workweek >= workdays)
                        {
                            continue;
                        }
                        if (resultFuncs[day] == null)
                        {
                            resultFuncs[day] = tempWorkdays[day].CountMaximizationFunction(worker);
                        }
                        resultFunc += resultFuncs[day] ?? 0;
                    }

                    if (resultFunc > maxResultFunc)
                    {
                        maxResultFunc = resultFunc;
                        bestOffset = offset;
                    }
                }

                for (var day = 0; day < tempWorkdays.Count; day++)
                {
                    if ((workweek + day - bestOffset) % workweek >= workdays)
                    {
                        continue;
                    }
                    tempWorkdays[day].InsertBest(worker);
                }
            }

            return this.Workdays;
        }
        
        private class Workday
        {
            private bool[] _workhoursItalian = new bool[14];
            private bool[] _workhoursRussian = new bool[14];
            private bool[] _workhoursJapanese = new bool[14];
            private List<DayGraphic> _workers = new List<DayGraphic>();
            
            public IEnumerable<DayGraphic> Workers
            {
                get => _workers;
            }

            public short CountMaximizationFunction(CookDTO worker)
            {
                short a, b;
                CookDTO.QualificationsType q;
                return this.CountMaximizationFunction(worker, out a, out b, out q);
            }
            
            private short CountMaximizationFunction(CookDTO worker,
                out short bestWorkdayStart,
                out short bestWorkdayEnd,
                out CookDTO.QualificationsType bestKitchen)
            {
                bestKitchen = CookDTO.QualificationsType.Italian;
                short workdayStart = worker.Shift == CookDTO.ShiftType.Morning ? (short)10 : (short)17;
                short workdayEnd = (short)(workdayStart + worker.WorkdayLength);
                var resultFunc = (short)0;
                if (workdayEnd > 24)
                {
                    workdayStart += (short)((short)24 - workdayEnd);
                    workdayEnd = 24;
                }
                bestWorkdayStart = workdayStart;
                bestWorkdayEnd = workdayEnd;
                short workdayTreshold = worker.Shift == CookDTO.ShiftType.Evening ? (short) 24 :
                    worker.WorkdayLength > 7 ? (short) (10 + worker.WorkdayLength) : (short)17;

                while (workdayEnd <= workdayTreshold)
                {
                    if (worker.Qualifications.Contains(CookDTO.QualificationsType.Italian))
                    {
                        var freeHours = _workhoursItalian.Skip(10 - workdayStart).Take(workdayEnd - workdayStart)
                            .Count(h => !h);
                        if (freeHours > resultFunc)
                        {
                            resultFunc = (short)freeHours;
                            bestKitchen = CookDTO.QualificationsType.Italian;
                            bestWorkdayStart = workdayStart;
                            bestWorkdayEnd = workdayEnd;
                        }
                    }
                    
                    if (worker.Qualifications.Contains(CookDTO.QualificationsType.Russian))
                    {
                        var freeHours = _workhoursRussian.Skip(10 - workdayStart).Take(workdayEnd - workdayStart)
                            .Count(h => !h);
                        if (freeHours > resultFunc)
                        {
                            resultFunc = (short)freeHours;
                            bestKitchen = CookDTO.QualificationsType.Russian;
                            bestWorkdayStart = workdayStart;
                            bestWorkdayEnd = workdayEnd;
                        }
                    }
                    
                    if (worker.Qualifications.Contains(CookDTO.QualificationsType.Japanese))
                    {
                        var freeHours = _workhoursJapanese.Skip(10 - workdayStart).Take(workdayEnd - workdayStart)
                            .Count(h => !h);
                        if (freeHours > resultFunc)
                        {
                            resultFunc = (short)freeHours;
                            bestKitchen = CookDTO.QualificationsType.Japanese;
                            bestWorkdayStart = workdayStart;
                            bestWorkdayEnd = workdayEnd;
                        }
                    }

                    workdayStart++;
                    workdayEnd++;
                }

                return resultFunc;
            }

            public void InsertBest(CookDTO worker)
            {
                short bestDayStart;
                short bestDayEnd;
                CookDTO.QualificationsType bestKitchen;
                this.CountMaximizationFunction(worker, out bestDayStart, out bestDayEnd, out bestKitchen);
                switch (bestKitchen)
                {
                    case CookDTO.QualificationsType.Italian:
                        for (var i = bestDayStart - 10; i < bestDayEnd - bestDayStart; i++)
                        {
                            _workhoursItalian[i] = true;
                        }
                        break;
                    case CookDTO.QualificationsType.Russian:
                        for (var i = bestDayStart - 10; i < bestDayEnd - bestDayStart; i++)
                        {
                            _workhoursRussian[i] = true;
                        }
                        break;
                    case CookDTO.QualificationsType.Japanese:
                        for (var i = bestDayStart - 10; i < bestDayEnd - bestDayStart; i++)
                        {
                            _workhoursJapanese[i] = true;
                        }
                        break;
                }
                this._workers.Add(new DayGraphic(bestKitchen, bestDayStart, bestDayEnd, worker));
            }

            public int GetZeroHours()
            {
                return this._workhoursItalian.Count(wh => !wh) +
                       this._workhoursRussian.Count(wh => !wh) +
                       this._workhoursJapanese.Count(wh => !wh);
            }
        }
    }

    public class DayGraphic
    {
        public CookDTO Worker { get; }
        public CookDTO.QualificationsType Kitchen { get; }
        public short DayStart { get;  }
        public short DayEnd { get;  }

        public DayGraphic(CookDTO.QualificationsType kitchen,
            short dayStart,
            short dayEnd,
            CookDTO worker)
        {
            if (dayEnd < 10 || dayEnd > 24 || dayStart < 10 || dayStart > 24 || dayEnd <= dayStart)
            {
                throw new ArgumentException(
                    "dayStart and dayEnd should be values from 10 to 24 and dayEnd should be greater dayStart");
            }
            this.DayStart = dayStart;
            this.DayEnd = dayEnd;
            this.Kitchen = kitchen;
            this.Worker = worker;
        }
    }
}