using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk
{
    public class EnglishLang : MonoBehaviour
    {
        public static Dictionary<string, string> englishDictionary = new Dictionary<string, string>()
        {
            {"welcome",  "Welcome!"},
            {"Test",  "Test"},
            {"Hi",  "Hi: "},
            { "Total Score",  "Total Score: "},
            { "Score",  "Score: "},
            { "Total",  "TOTAL: "},
            { "Victory",  "VICTORY"},
            { "Replay",  "Replay"},

            { "autoSave",  "...Auto Saving Complete"},
            { "newGame",  "New Game"},
            { "continue",  "Continue"},
            { "End",  "End"},

            { "Level",  "Level "},

            { "replayInfo", "Well done!\n You need at least one star to unlock the next level.\n You can then replay levels once more to get a better score."},
            { "replayInfo2", "You need at least one star to unlock the next level.\n You can replay levels until you get one star."},


            { "Congratulations1",  "Congratulations!<size=6>\n\n</size>You Win<size=6>\n\n</size>Score: "},
            { "Congratulations2",  "Congratulations!<size=6>\n\n</size>You are a PolyGonk<size=6>\n\n</size>Expert!<size=6>\n\n</size>Score: "},
            { "Congratulations3",  "Congratulations!<size=6>\n\n</size>You are a PolyGonk<size=6>\n\n</size>Champion!<size=6>\n\n</size>Score: "},
            { "Congratulations4",  "Congratulations!<size=6>\n\n</size>You are the PolyGonk<size=6>\n\n</size>MASTER!<size=6>\n\n</size>Score: "},

            { "Lesson1Slide1Welcome",  "Welcome to PolyGonk!"},
            { "Lesson1Slide1text1",  "Level 1: Polygons"},
            { "Lesson1Slide1text2",  "A polygon is a closed shape made only with straight sides."},
            { "Lesson1Slide1text3",  "Simple!"},

            { "Lesson1Slide2Welcome",  "Lesson 1: "},
            { "Lesson1Slide2text1",  "All polygons are shapes"},
            { "Lesson1Slide2text2",  "but not all shapes are polygons."},
            { "Lesson1Slide2text3",  ""},

            { "Lesson1Slide3Welcome",  "Lesson 1: "},
            { "Lesson1Slide3text1",  "Polygons are a subcategory of Shapes."},
            { "Lesson1Slide3text2",  "There are also subcategories of polygons."},
            { "Lesson1Slide3text3",  ""},

            { "Lesson1Slide4Welcome",  "Let's see how you are getting on."},
            { "Lesson1Slide4text1",  "Remember: All shapes with only straight sides are polygons."},
            { "Lesson1Slide4text2",  "Triangles, pentagons, rectangles and many more."},
            { "Lesson1Slide4text3",  "And all polygons are shapes!"},


            { "Lesson2Slide1Welcome",  "Level 2: Angles"},
            { "Lesson2Slide1text1",  "All polygons are made of sides and angles."},
            { "Lesson2Slide1text2",  "A polygon has the same number of sides and angles."},
            { "Lesson2Slide1text3",  "For example a triangle has 3 sides and 3 angles."},

            { "Lesson2Slide2Welcome",  "A quadrilateral has 4 sides and angles."},
            { "Lesson2Slide2text1",  "A pentagon has 5 sides and angles."},
            { "Lesson2Slide2text2",  "A hexagon has 6 sides and angles."},
            { "Lesson2Slide2text3",  "The number of sides and angles is key to naming shapes!"},

            { "Lesson2Slide3Welcome",  "Subcategories of shapes are often named after their angles."},
            { "Lesson2Slide3text1",  "For example a quadrilateral with all the same angles is a rectangle."},
            { "Lesson2Slide3text2",  "A square is a type of rectangle."},
            { "Lesson2Slide3text3",  "Shapes with all the same angles are called equiangular."},

            { "Lesson2Slide4Welcome",  "Two lines that go in the same direction are parallel."},
            { "Lesson2Slide4text1",  "A quadrilateral with one set of parallel lines is a trapezoid."},
            { "Lesson2Slide4text2",  "A quadrilateral with 2 sets of parallel lines is called a parallelogram."},
            { "Lesson2Slide4text3",  "Let's see how you're getting on!"},


            { "Lesson3Slide1Welcome",  "Lesson 3: Sides"},
            { "Lesson3Slide1text1",  "We have already learned that the number of sides is fundamental to categorising shapes."},
            { "Lesson3Slide1text2",  "A triangle has 3 sides, quadrilateral 4 sides, pentagon 5 sides and so on."},
            { "Lesson3Slide1text3",  "The length of sides is also very important to subcategories."},

            { "Lesson3Slide2Welcome",  "Taking triangles for example: "},
            { "Lesson3Slide2text1",  "A triangle with no sides the same is Scalene."},
            { "Lesson3Slide2text2",  "A triangle with 2 sides the same is Isosceles."},
            { "Lesson3Slide2text3",  "A triangle with all sides the same length is Equilateral."},


            { "Lesson4Slide1Welcome",  "Lesson 4: Subcategories"},
            { "Lesson4Slide1text1",  "Categories of shapes can have subcategories."},
            { "Lesson4Slide1text2",  "Those subcategories can have subcategories..."},
            { "Lesson4Slide1text3",  "and so on!"},

            { "Lesson4Slide2Welcome",  "The defining characteristics of a category must also apply to all the subcategories."},
            { "Lesson4Slide2text1",  "A quadrilateral is defined as having 4 sides."},
            { "Lesson4Slide2text2",  "Therefore a Rectangle has 4 sides. Rectangles are also defined as having equal angles."},
            { "Lesson4Slide2text3",  "Therefore a square has 4 sides AND equal angles. It is also defined as having equal length sides."},


            { "Lesson5Slide1Welcome",  "Lesson 5: Sides and Angles"},
            { "Lesson5Slide1text1",  "We have learned the importance of sides and angles."},
            { "Lesson5Slide1text2",  "Both are key to categorising shapes."},
            { "Lesson5Slide1text3",  "So you need to consider both!"},

            { "Lesson5Slide2Welcome",  "Triangles: "},
            { "Lesson5Slide2text1",  "Remember the sides of a triangle determine whether it is Scalene, Isosceles or Equilateral."},
            { "Lesson5Slide2text2",  "Also the angles determine whether it is Acute, Right-Angled or Obtuse."},
            { "Lesson5Slide2text3",  "Also the angles determine whether it is Acute, Right-Angled or Obtuse."},

            { "Lesson5Slide3Welcome",  "Triangles: "},
            { "Lesson5Slide3text1",  "For example a triangle could be both Obtuse and Isosceles"},
            { "Lesson5Slide3text2",  "or Right-Angled and Scalene"},
            { "Lesson5Slide3text3",  "Or Equilateral and Acute (Equilateral triangles are always Acute)."},

            { "Lesson5Slide4Welcome",  "Quadrilaterals: "},
            { "Lesson5Slide4text1",  "If a quadrilateral has equal angles it is a Rectangle."},
            { "Lesson5Slide4text2",  "If it has equal sides it is a Rhombus."},
            { "Lesson5Slide4text3",  "Only if it has both equal sides and equal angles is it Square."},

            { "Lesson5Slide5Welcome",  "Quadrilaterals: "},
            { "Lesson5Slide5text1",  ""},
            { "Lesson5Slide5text2",  ""},
            { "Lesson5Slide5text3",  ""},


            { "Lesson6Slide1Welcome",  "Lesson 6: Shared properties"},
            { "Lesson6Slide1text1",  "We have learned that the defining characteristics of a category MUST be inherited by its subcategories."},
            { "Lesson6Slide1text2",  "Other properties will also be inherited by subcategories."},
            { "Lesson6Slide1text3",  "They might also be shared with other categories!"},

            { "Lesson6Slide2Welcome",  "Inherited properties: "},
            { "Lesson6Slide2text1",  "You may observe that both Rectangles and Rhombuses are symmetrical."},
            { "Lesson6Slide2text2",  "You can infer that the subcategory Squares are also symmetrical."},
            { "Lesson6Slide2text3",  ""},

            { "Lesson6Slide3Welcome",  "Shared properties: "},
            { "Lesson6Slide3text1",  "A square is a Regular shape because it has both equal angles (equiangular) and equal sides (equilateral)."},
            { "Lesson6Slide3text2",  "It shares some properties with other regular shapes."},
            { "Lesson6Slide3text3",  ""},

            { "Lesson6Slide4Welcome",  "For example they all have the number of lines of symmetry matching the number of their sides."},
            { "Lesson6Slide4text1",  "They also have the same order of rotational symmetry as the number of their sides."},
            { "Lesson6Slide4text2",  ""},
            { "Lesson6Slide4text3",  ""},

            { "Lesson6Slide5Welcome",  "Let's mix things up a little."},
            { "Lesson6Slide5text1",  "There are two diving boards this time."},
            { "Lesson6Slide5text2",  "Good luck!"},
            { "Lesson6Slide5text3",  ""},


            { "Lesson7Slide1Welcome",  "Level 7: Finale"},
            { "Lesson7Slide1text1",  "You have nearly completed your PolyGonk journey."},
            { "Lesson7Slide1text2",  "Let's brush up on a few key points."},
            { "Lesson7Slide1text3",  ""},

            { "Lesson7Slide2Welcome",  "Review: "},
            { "Lesson7Slide2text1",  "Polygons are 2d shapes with all the sides straight."},
            { "Lesson7Slide2text2",  ""},
            { "Lesson7Slide2text3",  ""},

            { "Lesson7Slide3Welcome",  "Review: "},
            { "Lesson7Slide3text1",  ""},
            { "Lesson7Slide3text2",  "Polygons are categorised primarily by their number of sides/points."},
            { "Lesson7Slide3text3",  "They then have subcategories, particularly based on Sides and Angles."},

            { "Lesson7Slide4Welcome",  "Review: "},
            { "Lesson7Slide4text1",  "Subcategories inherit properties from their parent categories."},
            { "Lesson7Slide4text2",  ""},
            { "Lesson7Slide4text3",  ""},

            { "Lesson7Slide5Welcome",  "This is the final level."},
            { "Lesson7Slide5text1",  "There is no time limit!"},
            { "Lesson7Slide5text2",  "You have five lives..."},
            { "Lesson7Slide5text3",  "and no second chance. Good Luck!"},


            { "Level1Welcome1a",  "Welcome to the pool!"},
            { "Level1Welcome1b",  "The aim of the game is to categorise shapes."},
            { "Level1Welcome1c",  "You get 0 points for a false category,<size=6>\n\n</size>1 point for a correct category and<size=6>\n\n</size>2 points for the most specific category."},

            { "Level1Welcome2a",  "Shapes will appear on the diving board."},
            { "Level1Welcome2b",  "Use the touchscreen buttons (or arrow keys if you have a keyboard) to move the TRAMPOLINE."},
            { "Level1Welcome2c",  "BOUNCE the shapes into the best possible category."},

            { "Level1Welcome3a",  "Remember a Polygon has only straight sides."},
            { "Level1Welcome3b",  "If a shape has any curved sides then leave it in the Shapes lane."},
            { "Level1Welcome3c",  "Good luck!"},

            { "SHAPE",  "SHAPE"},
            { "POLYGON",  "POLYGON"},
            { "TRIANGLE",  "TRIANGLE"},
            { "QUADRILATERAL",  "QUADRILATERAL"},
            { "TRAPEZOID",  "TRAPEZOID"},
            { "PARALLELOGRAM",  "PARALLELOGRAM"},
            { "RECTANGLE",  "RECTANGLE"},
            { "SCALENE",  "SCALENE"},
            { "ISOSCELES",  "ISOSCELES"},
            { "EQUILATERAL",  "EQUILATERAL"},
            { "SQUARE",  "SQUARE"},
            { "RHOMBUS",  "RHOMBUS"},
            { "EQUIANGULAR", "EQUIANGULAR"},
            { "REGULAR", "REGULAR"},

            {"Perfect", "PERFECT!"}
        };
    }
}