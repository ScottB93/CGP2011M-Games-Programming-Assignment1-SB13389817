using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Audio;


namespace Escape
{
    class Rayman2 : Game
    {
        SpriteFont myFont;
        Vector2 myPos;
        Vector2 healthPos;
        Vector2 Velocity;
        int score = 0;
        int health = 3;
        //Sound
        SoundEffectInstance jumpVol, hurtVol, healthVol, orbVol, splashVol, finishVol;
        public SoundEffect jumpSound, hurtSound, healthSound, orbSound, splashSound, finishSound;
        

        //Bounding Variables #################################
        public Rectangle playerBoundingRec;
        Rectangle[] grass1Rectangles = new Rectangle[5];
        Rectangle[] grass2Rectangles = new Rectangle[5];
        Rectangle[] grass3Rectangles = new Rectangle[5];
        Rectangle[] platform1Rectangles = new Rectangle[5];
        Rectangle[] platform2Rectangles = new Rectangle[5];
        Rectangle[] cloudRectangle1 = new Rectangle[1];
        Rectangle[] cloudRectangle2 = new Rectangle[1];
        Rectangle[] cloudRectangle3 = new Rectangle[1];
        Rectangle[] smallPlatform1Rectangles = new Rectangle[5];
        Rectangle[] smallPlatform2Rectangles = new Rectangle[5];
        Rectangle[] orbRectangles5 = new Rectangle[1];
        Rectangle[] orbRectangles6 = new Rectangle[1];
        Rectangle[] orbRectangles7 = new Rectangle[1];
        Rectangle[] orbRectangles8 = new Rectangle[1];
        Rectangle[] heartRectangle = new Rectangle[1];
        Texture2D platform, cloud, smallPlatform, orb5, orb6, orb7, orb8;
        //###################################################

        Texture2D rightWalk, leftWalk, AnimationWalk, Water, finish2, dead,grassFloor,ladder,heart;
        Rectangle destRec;
        bool jumping;
        float StartJump, jumpSpeed = 0;
        public Rectangle sourceRec;
        float elasped;
        float delay = 100f;
        int frames = 0;
        KeyboardState oldKeyboardState, currentKeyboardState;
        public Vector2 positionPlayer;
        TimeSpan DeadTimer = new TimeSpan(0, 0, 5);

        public void Initialize()
        {

            currentKeyboardState = new KeyboardState();
            positionPlayer = new Vector2(1, 150);
            destRec = new Rectangle(0, 0, 72, 83);
            
            jumping = false;
            jumpSpeed = 0;
            StartJump = positionPlayer.Y;

        }
        //Player Collision
        Point playerFrameSize = new Point(72, 83);
        int playerCollisionRectOffset = 10;
        //Water Collision
        Point waterFrameSize = new Point(1075, 47);
        int waterCollisionRectOffest = 10;
        Vector2 waterPosition = new Vector2(0, 440);
       //Ladder Collision
        Point ladderFrameSize = new Point(78, 150);
        int ladderCollisionRectOffest = 10;
        Vector2 ladderPosition = new Vector2(120, 200);
        //Finish
        Point finish2FrameSize = new Point(35, 45);
        int finish2CollisionRectOffest = 10;
        Vector2 finish2Position = new Vector2(700, 140);
        //Water Collide Function #######################################################
        public bool WaterCollide()
        {
            Rectangle playerRec = new Rectangle(
                        (int)positionPlayer.X + playerCollisionRectOffset,
                            (int)positionPlayer.Y + playerCollisionRectOffset,
                                playerFrameSize.X - (playerCollisionRectOffset * 2),
                                    playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle waterRec = new Rectangle(
                         (int)waterPosition.X + waterCollisionRectOffest,
                            (int)waterPosition.Y + waterCollisionRectOffest,
                                waterFrameSize.X - (waterCollisionRectOffest * 2),
                                    waterFrameSize.Y - (waterCollisionRectOffest * 2));

            return playerRec.Intersects(waterRec);
        }
        //##############################################################################
        //ladder
        public bool LadderCollide()
        {
            Rectangle playerRec = new Rectangle(
                        (int)positionPlayer.X + playerCollisionRectOffset,
                            (int)positionPlayer.Y + playerCollisionRectOffset,
                                playerFrameSize.X - (playerCollisionRectOffset * 2),
                                    playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle ladderRec = new Rectangle(
                         (int)ladderPosition.X + ladderCollisionRectOffest,
                            (int)ladderPosition.Y + ladderCollisionRectOffest,
                                ladderFrameSize.X - (ladderCollisionRectOffest * 2),
                                    ladderFrameSize.Y - (ladderCollisionRectOffest * 2));

            return playerRec.Intersects(ladderRec);
        }
        
        //Finish Collide
        public bool FinishCollide()
        {
            Rectangle playerRec = new Rectangle(
                                             (int)positionPlayer.X + playerCollisionRectOffset,
                                                 (int)positionPlayer.Y + playerCollisionRectOffset,
                                                      playerFrameSize.X - (playerCollisionRectOffset * 2),
                                                          playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle finish2Rec = new Rectangle(
                      (int)finish2Position.X + finish2CollisionRectOffest,
                          (int)finish2Position.Y + finish2CollisionRectOffest,
                               finish2FrameSize.X - (finish2CollisionRectOffest * 2),
                                  finish2FrameSize.Y - (finish2CollisionRectOffest * 2));

            return playerRec.Intersects(finish2Rec);
        }
        public void LoadContent(ContentManager content)
        {
            //Sound###################

            jumpSound = content.Load<SoundEffect>("Jump");
            jumpVol = jumpSound.CreateInstance();
            jumpVol.IsLooped = false;
            jumpVol.Volume = 0.16f;

            hurtSound = content.Load<SoundEffect>("Hurt");
            hurtVol = hurtSound.CreateInstance();
            hurtVol.IsLooped = false;
            hurtVol.Volume = 0.16f;

            healthSound = content.Load<SoundEffect>("HealthSound");
            healthVol = healthSound.CreateInstance();
            healthVol.IsLooped = false;
            healthVol.Volume = 0.16f;

            orbSound = content.Load<SoundEffect>("OrbSound");
            orbVol = orbSound.CreateInstance();
            orbVol.IsLooped = false;
            orbVol.Volume = 0.16f;

            splashSound = content.Load<SoundEffect>("Splash");
            splashVol = splashSound.CreateInstance();
            splashVol.IsLooped = false;
            splashVol.Volume = 0.16f;

            finishSound = content.Load<SoundEffect>("FinishSound");
            finishVol = finishSound.CreateInstance();
            finishVol.IsLooped = false;
            finishVol.Volume = 0.16f;

            //######################

            leftWalk = content.Load<Texture2D>("RaymanLeft");
            rightWalk = content.Load<Texture2D>("RaymanRight");
            grassFloor = content.Load<Texture2D>("GrassFloor");
            ladder = content.Load<Texture2D>("Ladder");
            finish2 = content.Load<Texture2D>("Finish");
            dead = content.Load<Texture2D>("YAD");
            Water = content.Load<Texture2D>("wWater");
            AnimationWalk = rightWalk;

            myFont = content.Load<SpriteFont>("Font");
            myPos = new Vector2(80, 20);
            healthPos = new Vector2(690, 20);

            //Bounding Rectangles ##################################################################################
            for (int i = 0; i < 5; i++)
            {
                //GrassFloor bounding rectangle
                grassFloor = content.Load<Texture2D>("GrassFloor");
                Vector2 grass1Position = new Vector2(i * 25, 300);
                grass1Rectangles[i] = new Rectangle((int)grass1Position.X, (int)grass1Position.Y, 110, 76);

                Vector2 grass2Position = new Vector2(650 + i * 50, 150);
                grass2Rectangles[i] = new Rectangle((int)grass2Position.X, (int)grass2Position.Y, 110, 76);

                Vector2 grass3Position = new Vector2(550 + i * 50, 400);
                grass3Rectangles[i] = new Rectangle((int)grass3Position.X, (int)grass3Position.Y, 110, 76);
            }
            for (int i = 0; i < 1; i++)
            {
                //Platform1/Platform2/Platform3/SmallPlatform bounding rectangle
                platform = content.Load<Texture2D>("Platform1");
                Vector2 platform1Position = new Vector2(300 + i * 50, 270);
                platform1Rectangles[i] = new Rectangle((int)platform1Position.X, (int)platform1Position.Y, 50, 5);

                Vector2 platform2Position = new Vector2(180 + i * 50, 180);
                platform2Rectangles[i] = new Rectangle((int)platform2Position.X, (int)platform2Position.Y, 50, 5);

                cloud = content.Load<Texture2D>("Cloud");
                Vector2 cloudPosition = new Vector2(340 + i * 40, 130);
                cloudRectangle1[i] = new Rectangle((int)cloudPosition.X, (int)cloudPosition.Y, 30, 30);

                Vector2 cloud2Position = new Vector2(455 + i * 40, 130);
                cloudRectangle2[i] = new Rectangle((int)cloud2Position.X, (int)cloud2Position.Y, 30, 30);

                Vector2 cloud3Position = new Vector2(565 + i * 40, 130);
                cloudRectangle3[i] = new Rectangle((int)cloud3Position.X, (int)cloudPosition.Y, 30, 30);

                smallPlatform = content.Load<Texture2D>("SmallPlatform");
                Vector2 smallPlatformPosition = new Vector2(575 + i * 50, 320);
                smallPlatform1Rectangles[i] = new Rectangle((int)smallPlatformPosition.X, (int)smallPlatformPosition.Y, 40, 1);

                Vector2 smallPlatformPosition2 = new Vector2(465 + i * 50, 260);
                smallPlatform2Rectangles[i] = new Rectangle((int)smallPlatformPosition2.X, (int)smallPlatformPosition2.Y, 40, 1);
                //orbs
                orb5 = content.Load<Texture2D>("orb5");
                Vector2 orb5Position = new Vector2(320 + i * 50, 250);
                orbRectangles5[i] = new Rectangle((int)orb5Position.X, (int)orb5Position.Y, 40, 37);

                orb6 = content.Load<Texture2D>("orb6");
                Vector2 orb6Position = new Vector2(210 + i * 50, 180);
                orbRectangles6[i] = new Rectangle((int)orb6Position.X, (int)orb6Position.Y, 40, 37);

                orb7 = content.Load<Texture2D>("orb7");
                Vector2 orb7Position = new Vector2(600 + i * 50, 390);
                orbRectangles7[i] = new Rectangle((int)orb7Position.X, (int)orb7Position.Y, 40, 37);

                orb8 = content.Load<Texture2D>("orb8");
                Vector2 orb8Position = new Vector2(455 + i * 40, 120);
                orbRectangles8[i] = new Rectangle((int)orb8Position.X, (int)orb8Position.Y, 40, 37);
                //heart
                heart = content.Load<Texture2D>("Heart");
                Vector2 heart1Position = new Vector2(485 + i * 40, 110);
                heartRectangle[i] = new Rectangle((int)heart1Position.X, (int)heart1Position.Y, 26, 23);
            }
            playerBoundingRec = new Rectangle(50, 400, 72, 50);
        }
        //###########################################################################################################

        //Animation for player - Shows frame by frame
        public void Animation(GameTime gameTime)
        {
            if (elasped >= delay)
            {
                if (frames >= 10)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }

                elasped = 0;
            }
            sourceRec = new Rectangle(72 * frames, 0, 72, 83);
            elasped += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        public void Update(GameTime gameTime)
        {
            //Water Collide
            if (WaterCollide())
            {
                positionPlayer = new Vector2(1, 350);
                updateBoundingRectangle();
                this.health -= 1;
                splashSound.Play();
            }
            //Ladder collide
            if (LadderCollide())
            {
                
                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    positionPlayer.Y += -8;
                    updateBoundingRectangle();
                }
            }


            //Health
            if (health == 0)
            {

                if (DeadTimer > TimeSpan.Zero)
                {
                    DeadTimer -= gameTime.ElapsedGameTime;
                }
                if (DeadTimer < TimeSpan.Zero)
                {
                    DeadTimer = TimeSpan.Zero;
                    updateBoundingRectangle();
                    health = 3;
                    score = 0;
                    positionPlayer = new Vector2(1, 350);
                    dead.Dispose();

                }
            }

            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            //Move player Right
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                positionPlayer.X += 3f;
                updateBoundingRectangle();
                AnimationWalk = rightWalk;
                Animation(gameTime);

            }
            //Move player Left
            else if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                positionPlayer.X -= 3f;
                updateBoundingRectangle();
                AnimationWalk = leftWalk;
                Animation(gameTime);

            }
            else
            {
                sourceRec = new Rectangle(0, 0, 72, 83);
            }
            //Allows the player to jump
            if (jumping)
            {

                positionPlayer += Velocity;
                Velocity.Y += 1;
                if (positionPlayer.Y >= StartJump)
                {
                    positionPlayer.Y = StartJump;
                    jumping = false;
                }
                updateBoundingRectangle();
            }
            else
            {
                //Space bar to jump
                if (currentKeyboardState.IsKeyDown(Keys.Space))
                {

                    positionPlayer.Y -= 20;
                    Velocity.Y = -20;
                    jumping = true;
                    updateBoundingRectangle();
                    jumpSound.Play();

                }


            }
            destRec = new Rectangle((int)positionPlayer.X, (int)positionPlayer.Y, 72, 83);


            //Gravity - Player will move down at a speed of 5f
            positionPlayer.Y += 5;
            updateBoundingRectangle();

            for (int i = 0; i < 5; i++)
            {
                //Bounding between player and grassFloor
                if (playerBoundingRec.Intersects(grass1Rectangles[i]))
                {
                    //Stops the player and updates the bounding rectangle
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(grass2Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(grass3Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
            }
            for(int i = 0; i < 1; i++)
            {
                if (playerBoundingRec.Intersects(platform1Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(platform2Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(cloudRectangle1[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(cloudRectangle2[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(cloudRectangle3[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(smallPlatform1Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(smallPlatform2Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                    jumping = false;
                }
                if (playerBoundingRec.Intersects(orbRectangles5[i]))
                {
                    /*If player hits the bounding rectangle of the object it will dispose
                     change the position of the object and increase the score by 1 each time*/
                    orb5.Dispose();
                    orbRectangles5[i] = new Rectangle(0, 0, 0, 0);
                    score += 10;
                    orbSound.Play();
                }
                if (playerBoundingRec.Intersects(orbRectangles6[i]))
                {
                    orb6.Dispose();
                    orbRectangles6[i] = new Rectangle(0, 0, 0, 0);
                    score += 10;
                    orbSound.Play();
                }
                if (playerBoundingRec.Intersects(orbRectangles7[i]))
                {
                    orb7.Dispose();
                    orbRectangles7[i] = new Rectangle(0, 0, 0, 0);
                    score += 10;
                    orbSound.Play();
                }
                if (playerBoundingRec.Intersects(orbRectangles8[i]))
                {
                    orb8.Dispose();
                    orbRectangles8[i] = new Rectangle(0, 0, 0, 0);
                    score += 10;
                    orbSound.Play();
                }
                if (playerBoundingRec.Intersects(heartRectangle[i]))
                {
                    heart.Dispose();
                    heartRectangle[i] = new Rectangle(0, 0, 0, 0);
                    health += 1;
                    healthSound.Play();
                }
            }
        }

        //The players bounding rectangle follows the players position
        public void updateBoundingRectangle()
        {
            playerBoundingRec.X = (int)positionPlayer.X;
            playerBoundingRec.Y = (int)positionPlayer.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
           

            for (int i = 0; i < 5; i = i + 1)
            {
                //Draws the grassFloor 5 times at 50 pixels each
                Vector2 grass1Position = new Vector2(i * 25, 300);
                spriteBatch.Draw(grassFloor, grass1Position, Color.White);

                Vector2 grass2Position = new Vector2(650 + i * 50, 150);
                spriteBatch.Draw(grassFloor, grass2Position, Color.White);

                Vector2 grass3Position = new Vector2(550 + i * 50, 400);
                spriteBatch.Draw(grassFloor, grass3Position, Color.White);
            }
            for (int i = 0; i < 1; i++ )
            {
                Vector2 platform1Position = new Vector2(300 + i * 50, 270);
                spriteBatch.Draw(platform, platform1Position, Color.White);

                Vector2 platform2Position = new Vector2(180 + i * 50, 180);
                spriteBatch.Draw(platform, platform2Position, Color.White);

                Vector2 cloudPosition = new Vector2(340 + i * 540, 130);
                spriteBatch.Draw(cloud, cloudPosition, Color.White);

                Vector2 cloud2Position = new Vector2(455 + i * 40, 130);
                spriteBatch.Draw(cloud, cloud2Position, Color.White);

                Vector2 cloud3Position = new Vector2(565 + i * 40, 130);
                spriteBatch.Draw(cloud, cloud3Position, Color.White);

                Vector2 smallPlatformPosition = new Vector2(575 + i * 50, 320);
                spriteBatch.Draw(smallPlatform, smallPlatformPosition, Color.White);

                Vector2 smallPlatformPosition2 = new Vector2(465 + i * 50, 260);
                spriteBatch.Draw(smallPlatform, smallPlatformPosition2, Color.White);

                Vector2 orb5Position = new Vector2(320 + i * 50, 250);
                spriteBatch.Draw(orb5, orb5Position, Color.White);

                Vector2 orb6Position = new Vector2(210 + i * 50, 180);
                spriteBatch.Draw(orb6, orb6Position, Color.White);

                Vector2 orb7Position = new Vector2(600 + i * 50, 390);
                spriteBatch.Draw(orb7, orb7Position, Color.White);

                Vector2 orb8Position = new Vector2(455 + i * 40, 120);
                spriteBatch.Draw(orb8, orb8Position, Color.White);

                Vector2 heart1Position = new Vector2(485 + i * 40, 110);
                spriteBatch.Draw(heart, heart1Position, Color.White);

            }


                //Draws 

            spriteBatch.DrawString(myFont, "X " + score, myPos, Color.Purple);
            spriteBatch.DrawString(myFont, "X " + health, healthPos, Color.Purple);
            
            spriteBatch.Draw(AnimationWalk, destRec, sourceRec, Color.White);
            spriteBatch.Draw(Water, waterPosition, Color.White);
            spriteBatch.Draw(ladder, ladderPosition, Color.White);
            spriteBatch.Draw(finish2, finish2Position, Color.White);
            if (health == 0)
            {
                spriteBatch.Draw(dead, new Rectangle(150, 0, 420, 420), Color.White);

            }
            

        }
    }
}
