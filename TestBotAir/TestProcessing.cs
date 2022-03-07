using BotAir;
using System;
using TestBotAir.Mocks;
using Xunit;

namespace TestBotAir
{
    public class TestProcessing
    {
        const ZoneState 
            X = ZoneState.Obstructed, 
            _ = ZoneState.Free, 
            o = ZoneState.Unknown;

        [Fact]
        public void ScanASingleSquare_ReturnsRightGrid()
        {
            var map =
                "***\n" +
                "*>*\n" +
                "***";
            ZoneState [] expected = { 
                X, X, X,
                X, _, X,
                X, X, X
            };
            var test = new Processing(width:3, height:3, motion:new MotionStub(map));

            Assert.Equal(expected, test.Scan());
        }
        [Fact]
        public void ScanEmptyTwoSquares_ReturnsRightGrid()
        {
            var map =
                "****\n" +
                "*> *\n" +
                "****";
            ZoneState[] expected = {
                X, X, X, X,
                X, _, _, X,
                X, X, X, X
            };
            var test = new Processing(width: 4, height: 3, motion: new MotionStub(map));

            Assert.Equal(expected, test.Scan());
        }
        [Fact]
        public void ScanTwoSquares_ReturnsRightGrid()
        {
            var map =
                "****\n" +
                "*>**\n" +
                "****";
            ZoneState[] expected = {
                X, X, X, X,
                X, _, X, X,
                X, X, X, X
            };
            var test = new Processing(width: 4, height: 3, motion: new MotionStub(map));

            Assert.Equal(expected, test.Scan());
        }
        [Fact]
        public void ScanASingleLine_ReturnsRightGrid()
        {
            var map =
                "*********\n" +
                "*>  **  *\n" +
                "*********";
            ZoneState[] expected = {
                X, X, X, X, X, X, X, X, X,
                X, _, _, _, X, o, o, o, X,
                X, X, X, X, X, X, X, X, X
            };
            var test = new Processing(width: 9, height: 3, motion: new MotionStub(map));

            Assert.Equal(expected, test.Scan());
        }
        [Fact]
        public void ScanAFreeRectangle_ReturnsRightGrid()
        {
            var map =
                "******\n" +
                "*>   *\n" +
                "*    *\n" +
                "*    *\n" +
                "******";
            ZoneState[] expected = {
                X, X, X, X, X, X,
                X, _, _, _, _, X,
                X, _, _, _, _, X,
                X, _, _, _, _, X,
                X, X, X, X, X, X
            };
            var test = new Processing(width: 6, height: 5, motion: new MotionStub(map));

            Assert.Equal(expected, test.Scan());
        }
    }
}
