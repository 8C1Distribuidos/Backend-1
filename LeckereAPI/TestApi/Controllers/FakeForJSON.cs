using System.Collections.Generic;
using TestApi.Models;

namespace TestApi.Controllers{
    public class FakeForJSON{
        public List<Catalog> catalogs = new List<Catalog>{
            new Catalog{
                id=1,
                name="Vinos"
            },
            new Catalog{
                id=2,
                name="Destilados"
            }
        };
        public List<Clasification> clasifications = new List<Clasification>{
            new Clasification{
                id=1,
                name="Vino blanco",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=2,
                name="Vino espumoso",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=3,
                name="Vino tinto",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=4,
                name="Vino rosado",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=5,
                name="Vino champage",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=6,
                name="Vino postre",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=7,
                name="Vino sin alcohol",
                catalog = 
                new Catalog{
                id=1,
                name="Vinos"
                }
            },
            new Clasification{
                id=8,
                name="Brandy",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=9,
                name="Tequila",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=10,
                name="Cognac",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=11,
                name="Ginebra",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=12,
                name="Mezcal",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=13,
                name="Ron",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=14,
                name="Whisky",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            },
            new Clasification{
                id=15,
                name="Vodka",
                catalog = 
                new Catalog{
                id=2,
                name="Destilados"
                }
            }

        };
        public List<Product> products = new List<Product>{
            new Product{
                id=1,
                name="CosechaRara",
                image="Algo.png",
                cost=15.00f,
                clasification=
                new Clasification{
                id=12,
                name="Mezcal",
                catalog = 
                new Catalog{
                    id=2,
                    name="Destilados"
                }
                }   
            },
            new Product{
                id=2,
                name="HappyRussian",
                image="Suka.png",
                cost=1.40f,
                clasification=
                new Clasification{
                id=15,
                name="Vodka",
                catalog = 
                new Catalog{
                    id=2,
                    name="Destilados"
                }
                }
            },
            new Product{
                id=3,
                name="HappyBear",
                image="HugeBear.png",
                cost=56.00f,
                clasification=
                new Clasification{
                id=15,
                name="Vodka",
                catalog = 
                new Catalog{
                    id=2,
                    name="Destilados"
                }
                }
            }
        };
    }
}