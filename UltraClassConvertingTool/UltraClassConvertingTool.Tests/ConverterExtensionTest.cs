#region Copyright
//Copyright 2016 Morozov Dmitry

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
#endregion Copyright
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UltraClassConvertingTool.Tests
{
    /// <summary>
    /// Simple class for testing pruposes.
    /// </summary>
    public class A
    {
        /// <summary>
        /// Property A.
        /// </summary>
        public int MyPropertyA { get; set; }
    }

    /// <summary>
    /// Simple class for testing pruposes.
    /// </summary>
    public class B
    {
        /// <summary>
        /// Propery B.
        /// </summary>
        public int MyPropertyB { get; set; }
    }

    /// <summary>
    /// Simple class for testing pruposes.
    /// </summary>
    public class C
    {
        /// <summary>
        /// Property A.
        /// </summary>
        public int MyPropertyA { get; set; }

        /// <summary>
        /// Propery B.
        /// </summary>
        public int MyPropertyB { get; set; }

    }

    /// <summary>
    /// Simple class for testing pruposes.
    /// </summary>
    public class D
    {
        /// <summary>
        /// Propery B.
        /// </summary>
        public A MyPropertyA { get; set; }
    }

    /// <summary>
    /// Testing converter extension methods.
    /// </summary>
    [TestClass]
    public class ConverterExtensionTest
    {
        /// <summary>
        /// Convert A to B. Cause no exception. Nothing converted.
        /// </summary>
        [TestMethod]
        public void Simple_ConvertAToB_NoExceptionPropertieNotCopied()
        {
            // Arrange
            A a = new A();
            a.MyPropertyA = 3;
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            B convertedB = a.To<B>();

            //Assert
            Assert.AreNotEqual(convertedB.MyPropertyB, a.MyPropertyA);
        }

        /// <summary>
        /// Convert B to C. Cause no exception. Properties of b Converted.
        /// </summary>
        [TestMethod]
        public void Simple_ConvertBToC_PropertieConverted()
        {
            // Arrange            
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            C convertedB = b.To<C>();

            //Assert
            Assert.AreEqual(convertedB.MyPropertyB, b.MyPropertyB);
        }

        /// <summary>
        /// Convert A to D. Cause no exception. Types Mismatch. 
        /// Nothing Converted .No Exception.
        /// </summary>
        [TestMethod]
        public void Simple_ConvertAToD_TypesMismatchNothingConvertedNoException()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            D convertedA = a.To<D>();

            //Assert
            Assert.AreNotEqual(convertedA.MyPropertyA, a.MyPropertyA);
        }

        /// <summary>
        /// Convert A to A. Converted object is not the same with origin.
        /// </summary>
        [TestMethod]
        public void Simple_ConvertAToA_ConvertedObjectIsNotTheSame()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            A convertedA = a.To<A>();

            //Assert
            Assert.AreNotSame(a, convertedA);
        }

        /// <summary>
        /// Convert A to B.Cause exception becouse of missing 
        /// mypropertyB in 'A' class. Exception Throwed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(PropertyNotSettedException))]
        public void Stict_ConvertAToB_ExceptionThrowed()
        {
            // Arrange
            A a = new A();
            a.MyPropertyA = 3;
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            B convertedB = a.To<B>(StictFlags.Stictly);

        }

        /// <summary>
        /// Convert B to C. Cause exception because of missing mypropertyA 
        /// in B class. Exception Throwed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(PropertyNotSettedException))]
        public void Stict_ConvertBToC_ExceptionThrowed()
        {
            // Arrange            
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            C convertedB = b.To<C>(StictFlags.Stictly);

        }

        /// <summary>
        /// Convert A to D. Cause exception becouse of property type mismatch. 
        /// Exception Throwed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(PropertyNotSettedException))]
        public void Stict_ConvertAToD_TypesMismatchExceptionThrowed()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            D convertedA = a.To<D>(StictFlags.Stictly);

        }

        /// <summary>
        /// Convert A to A. Converted object is not the same with origin.
        /// </summary>
        [TestMethod]
        public void Stict_ConvertAToA_ConvertedObjectIsNotTheSame()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            A convertedA = a.To<A>(StictFlags.Stictly);

            //Assert
            Assert.AreNotSame(a, convertedA);
        }

        /// <summary>
        /// Convert A to B. Cause no exception. Nothing converted.
        /// </summary>
        [TestMethod]
        public void Forced_ConvertAToB_NoExceptionPropertieNotCopied()
        {
            // Arrange
            A a = new A();
            a.MyPropertyA = 3;
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            B convertedB = a.To<B>();

            //Assert
            Assert.AreNotEqual(convertedB.MyPropertyB, a.MyPropertyA);
        }

        /// <summary>
        /// Convert B to C. Cause no exception. Properties of b Converted.
        /// </summary>
        [TestMethod]
        public void Forced_ConvertBToC_PropertieConverted()
        {
            // Arrange            
            B b = new B();
            b.MyPropertyB = 4;

            // Act
            C convertedB = b.To<C>();

            //Assert
            Assert.AreEqual(convertedB.MyPropertyB, b.MyPropertyB);
        }

        /// <summary>
        /// Convert A to D. Cause no exception. Types Mismatch. 
        /// Nothing Converted .No Exception.
        /// </summary>
        [TestMethod]
        public void Forced_ConvertAToD_TypesMismatchNothingConvertedNoException()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            D convertedA = a.To<D>();

            //Assert
            Assert.AreNotEqual(convertedA.MyPropertyA, a.MyPropertyA);
        }

        /// <summary>
        /// Convert A to A. Converted object is not the same with origin.
        /// </summary>
        [TestMethod]
        public void Forced_ConvertAToA_ConvertedObjectIsNotTheSame()
        {
            // Arrange            
            A a = new A();
            a.MyPropertyA = 3;

            // Act
            A convertedA = a.To<A>();

            //Assert
            Assert.AreNotSame(a, convertedA);
        }
    }
}
