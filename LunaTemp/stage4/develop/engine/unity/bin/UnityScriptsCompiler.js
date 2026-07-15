if ( TRACE ) { TRACE( JSON.parse( '["PlayableSmokeController#init","PlayableSmokeController#Awake","PlayableSmokeController#Start","PlayableSmokeController#Update","PlayableSmokeController#TrySelectBlock","PlayableSmokeController#CompleteMiniLevel","PlayableSmokeController#OnCtaClicked","PlayableSmokeController#OnDestroy"]' ) ); }
/**
 * @version 1.0.9692.36320
 * @copyright anton
 * @compiler Bridge.NET 17.9.42-luna
 */
Bridge.assembly("UnityScriptsCompiler", function ($asm, globals) {
    "use strict";

    /*IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty start.*/
    Bridge.define("IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty", {
        inherits: [UnityEngine.MonoBehaviour]
    });
    /*IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty end.*/

    /*PlayableSmokeController start.*/
    Bridge.define("PlayableSmokeController", {
        inherits: [UnityEngine.MonoBehaviour],
        fields: {
            mainCamera: null,
            block: null,
            exitPoint: null,
            endCard: null,
            ctaButton: null,
            moveDuration: 0,
            completed: false,
            moving: false
        },
        ctors: {
            init: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#init", this ); }

                this.moveDuration = 0.35;
            }
        },
        methods: {
            /*PlayableSmokeController.Awake start.*/
            Awake: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#Awake", this ); }

                if (UnityEngine.Component.op_Equality(this.mainCamera, null)) {
                    this.mainCamera = UnityEngine.Camera.main;
                }

                if (UnityEngine.GameObject.op_Inequality(this.endCard, null)) {
                    this.endCard.SetActive(false);
                }

                if (UnityEngine.MonoBehaviour.op_Inequality(this.ctaButton, null)) {
                    this.ctaButton.onClick.AddListener(Bridge.fn.cacheBind(this, this.OnCtaClicked));
                }
            },
            /*PlayableSmokeController.Awake end.*/

            /*PlayableSmokeController.Start start.*/
            Start: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#Start", this ); }

                UnityEngine.Debug.Log$1("[Smoke] Playable smoke scene started.");

                Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.TutorialStarted);
            },
            /*PlayableSmokeController.Start end.*/

            /*PlayableSmokeController.Update start.*/
            Update: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#Update", this ); }

                if (this.completed || this.moving) {
                    return;
                }

                if (UnityEngine.Input.GetMouseButtonDown(0)) {
                    this.TrySelectBlock();
                }
            },
            /*PlayableSmokeController.Update end.*/

            /*PlayableSmokeController.TrySelectBlock start.*/
            TrySelectBlock: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#TrySelectBlock", this ); }

                var ray = this.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                var hit = { v : new UnityEngine.RaycastHit() };

                if (UnityEngine.Physics.Raycast$1(ray, hit)) {
                    if (UnityEngine.Component.op_Equality(hit.v.transform, this.block)) {
                        UnityEngine.Debug.Log$1("[Smoke] Block selected.");
                        this.StartCoroutine$1(this.CompleteMiniLevel());
                    }
                }
            },
            /*PlayableSmokeController.TrySelectBlock end.*/

            /*PlayableSmokeController.CompleteMiniLevel start.*/
            CompleteMiniLevel: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#CompleteMiniLevel", this ); }

                var $step = 0,
                    $jumpFromFinally,
                    $returnValue,
                    start,
                    target,
                    elapsed,
                    t,
                    $async_e;

                var $enumerator = new Bridge.GeneratorEnumerator(Bridge.fn.bind(this, function () {
                    try {
                        for (;;) {
                            switch ($step) {
                                case 0: {
                                    this.moving = true;

                                        start = this.block.position.$clone();
                                        target = this.exitPoint.position.$clone();
                                        elapsed = 0.0;
                                    $step = 1;
                                    continue;
                                }
                                case 1: {
                                    if ( elapsed < this.moveDuration ) {
                                            $step = 2;
                                            continue;
                                        } 
                                        $step = 4;
                                        continue;
                                }
                                case 2: {
                                    elapsed += UnityEngine.Time.deltaTime;
                                        t = Math.max(0, Math.min(1, elapsed / this.moveDuration));
                                        this.block.position = new pc.Vec3().lerp( start, target, t );
                                        $enumerator.current = null;
                                        $step = 3;
                                        return true;
                                }
                                case 3: {
                                    
                                        $step = 1;
                                        continue;
                                }
                                case 4: {
                                    this.block.position = target.$clone();

                                        this.completed = true;
                                        this.moving = false;

                                        UnityEngine.Debug.Log$1("[Smoke] Level won.");

                                        Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.LevelWon);

                                        if (UnityEngine.GameObject.op_Inequality(this.endCard, null)) {
                                            this.endCard.SetActive(true);
                                        }

                                        Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.EndCardShown);

                                        Luna.Unity.LifeCycle.GameEnded();

                                }
                                default: {
                                    return false;
                                }
                            }
                        }
                    } catch($async_e1) {
                        $async_e = System.Exception.create($async_e1);
                        throw $async_e;
                    }
                }));
                return $enumerator;
            },
            /*PlayableSmokeController.CompleteMiniLevel end.*/

            /*PlayableSmokeController.OnCtaClicked start.*/
            OnCtaClicked: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#OnCtaClicked", this ); }

                UnityEngine.Debug.Log$1("[Smoke] CTA clicked.");

                Luna.Unity.Analytics.LogEvent$1("CTA_Clicked", 1);

                Luna.Unity.Playable.InstallFullGame();
            },
            /*PlayableSmokeController.OnCtaClicked end.*/

            /*PlayableSmokeController.OnDestroy start.*/
            OnDestroy: function () {
if ( TRACE ) { TRACE( "PlayableSmokeController#OnDestroy", this ); }

                if (UnityEngine.MonoBehaviour.op_Inequality(this.ctaButton, null)) {
                    this.ctaButton.onClick.RemoveListener(Bridge.fn.cacheBind(this, this.OnCtaClicked));
                }
            },
            /*PlayableSmokeController.OnDestroy end.*/


        }
    });
    /*PlayableSmokeController end.*/

    if ( MODULE_reflection ) {
    var $m = Bridge.setMetadata,
        $n = ["System","System.Collections","UnityEngine","UnityEngine.UI"];

    /*PlayableSmokeController start.*/
    $m("PlayableSmokeController", function () { return {"att":1048577,"a":2,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"},{"a":1,"n":"Awake","t":8,"sn":"Awake","rt":$n[0].Void},{"a":1,"n":"CompleteMiniLevel","t":8,"sn":"CompleteMiniLevel","rt":$n[1].IEnumerator},{"a":2,"n":"OnCtaClicked","t":8,"sn":"OnCtaClicked","rt":$n[0].Void},{"a":1,"n":"OnDestroy","t":8,"sn":"OnDestroy","rt":$n[0].Void},{"a":1,"n":"Start","t":8,"sn":"Start","rt":$n[0].Void},{"a":1,"n":"TrySelectBlock","t":8,"sn":"TrySelectBlock","rt":$n[0].Void},{"a":1,"n":"Update","t":8,"sn":"Update","rt":$n[0].Void},{"at":[new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"block","t":4,"rt":$n[2].Transform,"sn":"block"},{"a":1,"n":"completed","t":4,"rt":$n[0].Boolean,"sn":"completed","box":function ($v) { return Bridge.box($v, System.Boolean, System.Boolean.toString);}},{"at":[new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"ctaButton","t":4,"rt":$n[3].Button,"sn":"ctaButton"},{"at":[new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"endCard","t":4,"rt":$n[2].GameObject,"sn":"endCard"},{"at":[new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"exitPoint","t":4,"rt":$n[2].Transform,"sn":"exitPoint"},{"at":[new UnityEngine.HeaderAttribute("Scene References"),new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"mainCamera","t":4,"rt":$n[2].Camera,"sn":"mainCamera"},{"at":[new UnityEngine.HeaderAttribute("Movement"),new UnityEngine.SerializeFieldAttribute()],"a":1,"n":"moveDuration","t":4,"rt":$n[0].Single,"sn":"moveDuration","box":function ($v) { return Bridge.box($v, System.Single, System.Single.format, System.Single.getHashCode);}},{"a":1,"n":"moving","t":4,"rt":$n[0].Boolean,"sn":"moving","box":function ($v) { return Bridge.box($v, System.Boolean, System.Boolean.toString);}}]}; }, $n);
    /*PlayableSmokeController end.*/

    /*IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty start.*/
    $m("IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty", function () { return {"att":1048577,"a":2,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"}]}; }, $n);
    /*IAmAnEmptyScriptJustToMakeCodelessProjectsCompileProperty end.*/

    }});
