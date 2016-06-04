﻿using InwersjaTomograficzna.Core.TraceRouting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InwersjaTomograficzna.Core.TraceRouting.DataStructures
{
    public class RoutedMatrix
    {
        private double[,] matrixOfEndValues;
        private int[] xBoarders;
        private int[] yBoarders;
        private List<Cell> matrixCells;
        private SignalRoutes allSignals;

        RoutedMatrix(int cellSize, SignalRoutes signals, int minX, int maxX, int minY, int maxY)
        {
            if (maxX % cellSize != 0 || maxY % cellSize != 0)
            {
                throw new FormatException("Wrong data! wrong cell size");
            }

            allSignals = signals;
            xBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minX, maxX, cellSize);
            yBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minY, maxY, cellSize);

            GenerateMatrix();
        }

        private int[] GetBoardersFromMinAndMaxValueAndCellSize(int min, int max, int cellSize)
        {
            List<int> boarders = new List<int>();
            
            for(int i = min; i <= max; i++)
            {
                boarders.Add(i);
            }

            return boarders.ToArray();
        }

        private void GenerateMatrix()
        {
            matrixCells = new List<Cell>();

            matrixOfEndValues = new double[xBoarders.Length - 1, yBoarders.Length - 1];

            for(int xIndex = 0; xIndex < xBoarders.Length-1; xIndex++)
            {
                for(int yIndex = 0; yIndex < yBoarders.Length - 1; yIndex++)
                {
                    matrixCells.Add(
                        new Cell(xIndex, yIndex,
                            xBoarders[xIndex], xBoarders[xIndex + 1],
                            yBoarders[yIndex], yBoarders[yIndex + 1]));
                }
            }
        }

        public double[,] MakeTraceRouting()
        {
            List<Point> temporaryPoints;

            foreach(var signal in allSignals.AllRoutes)
            {
                temporaryPoints = new List<Point>();
                foreach(var xAxis in xBoarders)
                {
                    temporaryPoints.Add(signal.GetCrossPointForXAxis(xAxis));
                }
                foreach(var yAxis in yBoarders)
                {
                    temporaryPoints.Add(signal.GetCrossPointForYAxis(yAxis));
                }
                List<Point> sortedPoints = PointsSort.SortByDistanceFromPoint(signal.StartPoint, temporaryPoints.Distinct().ToList());

                AddSignalLengthsToCells(sortedPoints);
            }

            return matrixOfEndValues;
        }

        private void AddSignalLengthsToCells(List<Point> sortedListofCorssPoints)
        {
            for(int i=0; i<sortedListofCorssPoints.Count()-1; i++)
            {
                double distance = sortedListofCorssPoints[i].Distance(sortedListofCorssPoints[i + 1]);
                AddValueToSpecificCell(sortedListofCorssPoints[i], sortedListofCorssPoints[i + 1], distance);
            }
        }

        private void AddValueToSpecificCell(Point firstPoint, Point secondPoint, double value)
        {
            var cell = matrixCells.Where(cell1 => cell1.leftBoarder == firstPoint.X ||
                                                  cell1.rightBoarder == firstPoint.X ||
                                                  cell1.upperBoarder == firstPoint.Y ||
                                                  cell1.lowerBoarder == firstPoint.Y)
                                  .Where(cell2 => cell2.leftBoarder == secondPoint.X || 
                                                  cell2.rightBoarder == secondPoint.X || 
                                                  cell2.upperBoarder == secondPoint.Y || 
                                                  cell2.lowerBoarder == secondPoint.Y)
                                  .Single();
            matrixOfEndValues[cell.xIndex, cell.yIndex] += value;
        }
    }
}