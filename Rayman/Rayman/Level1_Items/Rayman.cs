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
using Microsoft.Xna.Framework.Media;

//using Microsoft.Xna.Framework.GamerServices;

namespace Escape
{
    class Rayman :Game
    {
        SpriteFont myFont;
        Vector2 myPos;
        Vector2 healthPos;
        int score = 0;
        int health = 3;
        
        //Sound
        SoundEffectInstance jumpVol,hurtVol,healthVol,orbVol,splashVol,finishVol;
        public SoundEffect jumpSound,hurtSound,healthSound,orbSound,splashSound,finishSound;

       //Bounding Variables #################################
        public Rectangle playerBoundingRec;
        Rectangle[] grass1Rectangles = new Rectangle[5];
        Rectangle[] grass2Rectangles = new Rectangle[5];
        Rectangle[] platform1Rectangles = new Rectangle[5];
        Rectangle[] platform2Rectangles = new Rectangle[5];
        Rectangle[] platform3Rectangles = new Rectangle[5];
        Rectangle[] smallPlatform1Rectangles = new Rectangle[5];
        Rectangle[] orbRectangles = new Rectangle[1];
        Rectangle[] orbRectangles2 = new Rectangle[1];
        Rectangle[] orbRectangles3 = new Rectangle[1];
        Rectangle[] orbRectangles4 = new Rectangle[1];
        Rectangle[] heartRectangle = new Rectangle[1];
        Rectangle[] cloudRectangle1 = new Rectangle[1];
        Rectangle[] cloudRectangle2 = new Rectangle[1];
        Rectangle[] cloudRectangle3 = new Rectangle[1];
        Texture2D grassFloor, platform, heart, spike1,spike2, smallPlatform, spine1,spine2,spine3, cloud;
        //###################################################
   
        Texture2D rightWalk, leftWalk, AnimationWalk, Water, orb, orb2,orb3,orb4, finish,dead;
        Rectangle destRec;
        bool jumping;
        float StartJump, jumpSpeed = 0;
        public Rectangle sourceRec;
        float elasped;
        Vector2 Velocity;
        float delay = 100f;
        int frames = 0;
        KeyboardState oldKeyboardState, currentKeyboardState;
        public Vector2 positionPlayer;
        TimeSpan DeadTimer = new TimeSpan(0, 0, 5);
      
    public void Initialize()
        {
            
            currentKeyboardState = new KeyboardState();
            positionPlayer = new Vector2(1,350);
            destRec = new Rectangle(0,0,72,83);
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
    //Spike1
    Point spike1FrameSize = new Point(18,24);
    int spike1CollisionRectOffest = 10;
    Vector2 spike1Position = new Vector2(350,275);
    //Spike2
    Point spike2FrameSize = new Point(18, 24);
    int spike2CollisionRectOffest = 10;
    Vector2 spike2Position = new Vector2(120, 200);
    //Spine1
    Point spine1FrameSize = new Point(5, 180);
    int spine1CollisionRectOffest = 10;
    Vector2 spine1Position = new Vector2(285, 10);
    //Spine2
    Point spine2FrameSize = new Point(5, 180);
    int spine2CollisionRectOffest = 10;
    Vector2 spine2Position = new Vector2(320, -45);
    //Spine3
    Point spine3FrameSize = new Point(5, 180);
    int spine3CollisionRectOffest = 10;
    Vector2 spine3Position = new Vector2(350, -100);
    //Finish
    Point finish1FrameSize = new Point(35, 45);
    int finish1CollisionRectOffest = 10;
    Vector2 finish1Position = new Vector2(750, 400);
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

    //Spike Collide Function ###########################################################
        public bool Spike1Collide()
    {
        Rectangle playerRec = new Rectangle(
                      (int)positionPlayer.X + playerCollisionRectOffset,
                          (int)positionPlayer.Y + playerCollisionRectOffset,
                               playerFrameSize.X - (playerCollisionRectOffset * 2),
                                   playerFrameSize.Y - (playerCollisionRectOffset * 2));

        Rectangle spike1Rec = new Rectangle(
                      (int)spike1Position.X + spike1CollisionRectOffest,
                          (int)spike1Position.Y + spike1CollisionRectOffest,
                               spike1FrameSize.X - (spike1CollisionRectOffest * 2),
                                  spike1FrameSize.Y - (spike1CollisionRectOffest * 2));

        return playerRec.Intersects(spike1Rec);

    }
    //###################################################################################
        //Spike2 Collide Function ###########################################################
        public bool Spike2Collide()
        {
            Rectangle playerRec = new Rectangle(
                          (int)positionPlayer.X + playerCollisionRectOffset,
                              (int)positionPlayer.Y + playerCollisionRectOffset,
                                   playerFrameSize.X - (playerCollisionRectOffset * 2),
                                       playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle spike2Rec = new Rectangle(
                          (int)spike2Position.X + spike2CollisionRectOffest,
                              (int)spike2Position.Y + spike2CollisionRectOffest,
                                   spike2FrameSize.X - (spike2CollisionRectOffest * 2),
                                      spike2FrameSize.Y - (spike2CollisionRectOffest * 2));

            return playerRec.Intersects(spike2Rec);

        }
        //###################################################################################
        //Spine 
        public bool Spine1Collide()
        {
            Rectangle playerRec = new Rectangle(
                                  (int)positionPlayer.X + playerCollisionRectOffset,
                                      (int)positionPlayer.Y + playerCollisionRectOffset,
                                           playerFrameSize.X - (playerCollisionRectOffset * 2),
                                               playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle spineRec = new Rectangle(
                      (int)spine1Position.X + spine1CollisionRectOffest,
                          (int)spine1Position.Y + spine1CollisionRectOffest,
                               spine1FrameSize.X - (spine1CollisionRectOffest * 2),
                                  spine1FrameSize.Y - (spine1CollisionRectOffest * 2));

            return playerRec.Intersects(spineRec);
        }
        //Spine2
        public bool Spine2Collide()
        {
            Rectangle playerRec = new Rectangle(
                                  (int)positionPlayer.X + playerCollisionRectOffset,
                                      (int)positionPlayer.Y + playerCollisionRectOffset,
                                           playerFrameSize.X - (playerCollisionRectOffset * 2),
                                               playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle spine2Rec = new Rectangle(
                      (int)spine2Position.X + spine2CollisionRectOffest,
                          (int)spine2Position.Y + spine2CollisionRectOffest,
                               spine2FrameSize.X - (spine2CollisionRectOffest * 2),
                                  spine2FrameSize.Y - (spine2CollisionRectOffest * 2));

            return playerRec.Intersects(spine2Rec);
        }
        //Spine3
        public bool Spine3Collide()
        {
            Rectangle playerRec = new Rectangle(
                                  (int)positionPlayer.X + playerCollisionRectOffset,
                                      (int)positionPlayer.Y + playerCollisionRectOffset,
                                           playerFrameSize.X - (playerCollisionRectOffset * 2),
                                               playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle spine3Rec = new Rectangle(
                      (int)spine3Position.X + spine3CollisionRectOffest,
                          (int)spine3Position.Y + spine3CollisionRectOffest,
                               spine3FrameSize.X - (spine3CollisionRectOffest * 2),
                                  spine3FrameSize.Y - (spine3CollisionRectOffest * 2));

            return playerRec.Intersects(spine3Rec);
        }
        //Finish Collide
        public bool FinishCollide()
        {
            Rectangle playerRec = new Rectangle(
                                             (int)positionPlayer.X + playerCollisionRectOffset,
                                                 (int)positionPlayer.Y + playerCollisionRectOffset,
                                                      playerFrameSize.X - (playerCollisionRectOffset * 2),
                                                          playerFrameSize.Y - (playerCollisionRectOffset * 2));

            Rectangle finish1Rec = new Rectangle(
                      (int)finish1Position.X + finish1CollisionRectOffest,
                          (int)finish1Position.Y + finish1CollisionRectOffest,
                               finish1FrameSize.X - (finish1CollisionRectOffest * 2),
                                  finish1FrameSize.Y - (finish1CollisionRectOffest * 2));

            return playerRec.Intersects(finish1Rec);
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
            //Loading Textures
            leftWalk = content.Load<Texture2D>("RaymanLeft");
            rightWalk = content.Load<Texture2D>("RaymanRight");
            grassFloor = content.Load<Texture2D>("GrassFloor");
            spike1 = content.Load<Texture2D>("Spike");
            spike2 = content.Load<Texture2D>("Spike");
            spine1 = content.Load<Texture2D>("Spine");
            spine2 = content.Load<Texture2D>("Spine");
            spine3 = content.Load<Texture2D>("Spine");
            Water = content.Load<Texture2D>("wWater");
            finish = content.Load<Texture2D>("Finish");
            dead = content.Load<Texture2D>("YAD");
            AnimationWalk = rightWalk;

            myFont = content.Load<SpriteFont>("Font");
            myPos = new Vector2(80, 20);
            healthPos = new Vector2(690, 20);
           
            //Bounding Rectangles ##################################################################################
            for (int i = 0; i < 5; i++)
            {
                //GrassFloor bounding rectangle
                grassFloor = content.Load<Texture2D>("GrassFloor");
                Vector2 grass1Position = new Vector2( i * 50, 400);
                grass1Rectangles[i] = new Rectangle((int)grass1Position.X, (int)grass1Position.Y, 110, 76);

                Vector2 grass2Position = new Vector2(650+i * 50, 400);
                grass2Rectangles[i] = new Rectangle((int)grass2Position.X, (int)grass2Position.Y, 110, 76);


            }
            for (int i = 0; i < 1; i++)
            {
                //Platform1/Platform2/Platform3/SmallPlatform bounding rectangle
                platform = content.Load<Texture2D>("Platform1");
                Vector2 platform1Position = new Vector2(320 + i * 5, 280);
                platform1Rectangles[i] = new Rectangle((int)platform1Position.X, (int)platform1Position.Y, 50, 5);

                Vector2 platform2Position = new Vector2(85 + i * 50, 200);
                platform2Rectangles[i] = new Rectangle((int)platform2Position.X, (int)platform2Position.Y, 50, 5);

                Vector2 platform3Position = new Vector2(600 + i * 50, 250);
                platform3Rectangles[i] = new Rectangle((int)platform3Position.X, (int)platform3Position.Y, 50, 5);

                smallPlatform = content.Load<Texture2D>("SmallPlatform");
                Vector2 smallPlatformPosition = new Vector2(265 + i * 5, 140);
                smallPlatform1Rectangles[i] = new Rectangle((int)smallPlatformPosition.X, (int)smallPlatformPosition.Y, 40, 1);

                //Orbs bounding rectangle
                orb = content.Load<Texture2D>("orb");
                Vector2 orb1Position = new Vector2(200 + i * 50, 380);
                orbRectangles[i] = new Rectangle((int)orb1Position.X, (int)orb1Position.Y, 40, 37);

                orb2 = content.Load<Texture2D>("orb2");
                Vector2 orb2Position = new Vector2(85 + i * 50, 180);
                orbRectangles2[i] = new Rectangle((int)orb2Position.X, (int)orb2Position.Y, 40, 37);

                orb3 = content.Load<Texture2D>("orb3");
                Vector2 orb3Position = new Vector2(255 + i * 50, 150);
                orbRectangles3[i] = new Rectangle((int)orb3Position.X, (int)orb3Position.Y, 40, 37);

                orb4 = content.Load<Texture2D>("orb4");
                Vector2 orb4Position = new Vector2(590 + i * 40, 110);
                orbRectangles4[i] = new Rectangle((int)orb4Position.X, (int)orb4Position.Y, 40, 37);
                //Heart bounding rectangle
                heart = content.Load<Texture2D>("Heart");
                Vector2 heart1Position = new Vector2(485 + i * 40, 110);
                heartRectangle[i] = new Rectangle((int)heart1Position.X, (int)heart1Position.Y, 26, 23);
                //cloud bounding rectangle
                cloud = content.Load<Texture2D>("Cloud");
                Vector2 cloudPosition = new Vector2(375 + i * 40, 130);
                cloudRectangle1[i] = new Rectangle((int)cloudPosition.X, (int)cloudPosition.Y, 30, 30);

                Vector2 cloud2Position = new Vector2(490 + i * 40, 130);
                cloudRectangle2[i] = new Rectangle((int)cloud2Position.X, (int)cloud2Position.Y, 30, 30);

                Vector2 cloud3Position = new Vector2(605 + i * 40, 130);
                cloudRectangle3[i] = new Rectangle((int)cloud3Position.X, (int)cloudPosition.Y, 30, 30);

            }
                //Players bounding rectangle
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
            
            //Water Coolide
            if (WaterCollide())
            {
                positionPlayer = new Vector2(1, 350);
                updateBoundingRectangle();
                this.health -= 1;
                this.score -= 5;
                splashSound.Play();
            }

            //Spike Collide
            if (Spike1Collide())
            {
                positionPlayer = new Vector2(1, 350);
                updateBoundingRectangle();
                health -= 1;
                score -= 5;
                hurtSound.Play();
            }
            //Spike2 Collide
            if (Spike2Collide())
            {
                positionPlayer = new Vector2(1, 350);
                updateBoundingRectangle();
                health -= 1;
                score -= 5;
                hurtSound.Play();
            }
            //Spine collide
            if (Spine1Collide())
            {

                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    positionPlayer.Y += -8;
                    updateBoundingRectangle();
                }
            }
            //Spine 2 collide
            if (Spine2Collide())
            {
                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    positionPlayer.Y += -8;
                    updateBoundingRectangle();
                }
            }
            //Spine 3 collide
            if (Spine3Collide())
            {

                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    positionPlayer.Y += -8;
                    updateBoundingRectangle();
                }
            }

            //Health == 0 reset
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
        
           //Jump
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
                if (currentKeyboardState.IsKeyDown(Keys.Space) )
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
                  
                }
                if(playerBoundingRec.Intersects(grass2Rectangles[i]))
                {
                    positionPlayer.Y -= 5;
                    updateBoundingRectangle();
                   
                }
                
            }
            for (int i = 0; i < 1; i++)
            {
                //Bounding between player and platforms
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
                    if (playerBoundingRec.Intersects(platform3Rectangles[i]))
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
                    //Bounding between player, orbs and hearts
                    if (playerBoundingRec.Intersects(orbRectangles[i]))
                    {
                        /*If player hits the bounding rectangle of the object it will dispose
                         change the position of the object and increase the score by 1 each time*/
                        orb.Dispose();
                        orbRectangles[i] = new Rectangle(0, 0, 0, 0);
                        score += 10;
                        orbSound.Play();

                    }
                //If the play intersects the objects Rectangles
                    if (playerBoundingRec.Intersects(orbRectangles2[i]))
                    {
                        orb2.Dispose();
                        orbRectangles2[i] = new Rectangle(0, 0, 0, 0);
                        score += 10;
                        orbSound.Play();
                    }
                    if (playerBoundingRec.Intersects(orbRectangles3[i]))
                    {
                        orb3.Dispose();
                        orbRectangles3[i] = new Rectangle(0, 0, 0, 0);
                        score += 10;
                        orbSound.Play();
                    }
                    if (playerBoundingRec.Intersects(orbRectangles4[i]))
                    {
                        orb4.Dispose();
                        orbRectangles4[i] = new Rectangle(0, 0, 0, 0);
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
 
            for (int i = 0; i < 5; i = i + 1)
            {
                //Draws the grassFloor 5 times at 50 pixels each
                Vector2 grass1Position = new Vector2(i * 50, 400);
                spriteBatch.Draw(grassFloor, grass1Position, Color.White);

                Vector2 grass2Position = new Vector2(650+i * 50, 400);
                spriteBatch.Draw(grassFloor, grass2Position, Color.White);

                
            }
             for (int i = 0; i < 1; i++)
            {
                 //Draws platforms/orb/heart once at 50 pixels each
                Vector2 platform1Position = new Vector2(300 + i * 50, 270);
                spriteBatch.Draw(platform, platform1Position, Color.White);

                Vector2 platform2Position = new Vector2(85 + i * 50, 200);
                spriteBatch.Draw(platform, platform2Position, Color.White);

                Vector2 platform3Position = new Vector2(600 + i * 50, 250);
                spriteBatch.Draw(platform, platform3Position, Color.White);

                 Vector2 smallPlatformPosition = new Vector2(255+ i * 50, 150);
                 spriteBatch.Draw(smallPlatform,smallPlatformPosition,Color.White);

                Vector2 orb1Position = new Vector2(200 + i * 50, 380);
                spriteBatch.Draw(orb, orb1Position, Color.White);

                Vector2 orb2Position = new Vector2(85 + i * 50, 200);
                spriteBatch.Draw(orb2, orb2Position, Color.White);

                Vector2 orb3Position = new Vector2(255 + i * 50, 150);
                spriteBatch.Draw(orb3, orb3Position, Color.White);

                Vector2 orb4Position = new Vector2(590 + i * 40, 110);
                spriteBatch.Draw(orb4, orb4Position, Color.White);

                Vector2 heart1Position = new Vector2(485 + i * 40, 110);
                spriteBatch.Draw(heart, heart1Position, Color.White);

                Vector2 cloudPosition = new Vector2(375 + i * 540, 130);
                spriteBatch.Draw(cloud, cloudPosition, Color.White);

                Vector2 cloud2Position = new Vector2(475 + i * 40, 130);
                spriteBatch.Draw(cloud, cloud2Position, Color.White);

                Vector2 cloud3Position = new Vector2(575 + i * 40, 130);
                spriteBatch.Draw(cloud, cloud3Position, Color.White);
            }
           
            //Draws 
            
            spriteBatch.DrawString(myFont, "X " + score, myPos, Color.Purple);
            spriteBatch.DrawString(myFont, "X " + health, healthPos, Color.Purple);
            spriteBatch.Draw(spine1, spine1Position, Color.White);
            spriteBatch.Draw(spine2, spine2Position, Color.White);
            spriteBatch.Draw(spine3, spine3Position, Color.White);
            spriteBatch.Draw(AnimationWalk, destRec, sourceRec, Color.White);
            spriteBatch.Draw(Water, waterPosition, Color.White);
            spriteBatch.Draw(spike1, spike1Position, Color.White);
            spriteBatch.Draw(spike2, spike2Position, Color.White); 
            spriteBatch.Draw(finish, finish1Position, Color.White);
            //If health is == 0 draw dead
            if (health == 0)
            {
                spriteBatch.Draw(dead, new Rectangle(150, 0, 420, 420), Color.White);

            }
           // spriteBatch.End();
            
        }
    }
}
